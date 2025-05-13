using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ActionableMessagesBackend
{
    public class Message
    {
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IBot _bot;
        public Message(IBotFrameworkHttpAdapter adapter, IBot bot)
        {
            _adapter = adapter;
            _bot = bot;
        }

        [Function("messages")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Message");

            // 1. Extract token
            if (!req.Headers.TryGetValue("Authorization", out var authHeader))
                return new UnauthorizedResult();

            var token = authHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            // 2. Validate Actionable Message JWT
            var isValid = await ValidateOutlookTokenAsync(token, logger);
            if (!isValid)
                return new UnauthorizedResult();

            // 3. Proceed with normal bot framework processing
            await _adapter.ProcessAsync(req, req.HttpContext.Response, _bot);
            return new OkResult();
        }

        private async Task<bool> ValidateOutlookTokenAsync(string token, ILogger logger)
        {
            try
            {
                var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    "https://login.botframework.com/v1/.well-known/openidconfiguration",
                    new OpenIdConnectConfigurationRetriever());

                var config = await configurationManager.GetConfigurationAsync();

                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuers = new[] { "https://api.botframework.com" },

                    ValidateAudience = true,
                    // Either match audience to a specific app or accept all if you use domain-based trust
                    ValidAudience = "e2aecf30-6400-4773-999d-314a6db1301c",

                    ValidateLifetime = true,

                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = config.SigningKeys
                };


                tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Token validation failed: {ex.Message}");
                return false;
            }
        }

        public static async Task<IEnumerable<SecurityKey>> GetSigningKeysAsync()
        {
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                "https://api.office.com/discovery/v2.0/keys",
                new OpenIdConnectConfigurationRetriever());

            var config = await configManager.GetConfigurationAsync(CancellationToken.None);
            return config.SigningKeys;
        }
    }
}
