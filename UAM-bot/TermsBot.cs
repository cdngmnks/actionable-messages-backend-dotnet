// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json.Linq;
using System.Linq;
using UAM_bot.Helpers;
using UAM_bot.Models;
using Microsoft.Extensions.Caching.Memory;

namespace UAM_bot
{
    public class TermsBot : ActivityHandler
    {

        bool? currentState = null;
        IMemoryCache _memoryCache;
        string cacheKey = "currentState";
        MemoryCacheEntryOptions cacheOptions;
        public TermsBot(IMemoryCache memoryCache) 
        {
            _memoryCache = memoryCache;
            cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTimeOffset.Now.AddMonths(1);
        }

        protected override async Task<AdaptiveCardInvokeResponse> OnAdaptiveCardInvokeAsync(ITurnContext<IInvokeActivity> turnContext, AdaptiveCardInvokeValue invokeValue, CancellationToken cancellationToken)
        {

            if (turnContext.Activity.Name == "adaptiveCard/action")
            {
                AdaptiveCardInvokeResponse adaptiveCardResponse;
                string cardJson;
                JObject response;

                var data = JsonConvert.DeserializeObject<InitialSequentialCard>(turnContext.Activity.Value.ToString());
                string verb = data.action.verb;

                switch (verb)
                {
                    case "termsAccept":
                        _memoryCache.Set(cacheKey, true, cacheOptions);

                        string[] approvedCard = { "adaptiveCardAccept.json" };
                        cardJson = CardHelper.GetJson(approvedCard);
                        response = JObject.Parse(cardJson);

                        adaptiveCardResponse = new AdaptiveCardInvokeResponse()
                        {
                            StatusCode = 200,
                            Type = AdaptiveCard.ContentType,
                            Value = response
                        };

                        return adaptiveCardResponse;

                    case "termsDecline":
                        _memoryCache.Set(cacheKey, false, cacheOptions);

                        string[] rejectedCard = { "adaptiveCardDecline.json" };
                        cardJson = CardHelper.GetJson(rejectedCard);
                        response = JObject.Parse(cardJson);

                        adaptiveCardResponse = new AdaptiveCardInvokeResponse()
                        {
                            StatusCode = 200,
                            Type = AdaptiveCard.ContentType,
                            Value = response
                        };

                        return adaptiveCardResponse;

                    case "initialRefresh":

                        _memoryCache.TryGetValue(cacheKey, out currentState);
                        if (currentState != null) 
                        {
                            string[] card;

                            if (currentState == true)
                            {
                                card = new string[] { "adaptiveCardAccept.json" };
                            } 
                            else
                            {
                                card = new string[] { "adaptiveCardDecline.json" };
                            }

                            cardJson = CardHelper.GetJson(card);
                            response = JObject.Parse(cardJson);

                            adaptiveCardResponse = new AdaptiveCardInvokeResponse()
                            {
                                StatusCode = 200,
                                Type = AdaptiveCard.ContentType,
                                Value = response
                            };

                            return adaptiveCardResponse;
                        }  
                        else
                        {
                            return null;
                        }
                }
            }

            return null;
        }
    }
}
