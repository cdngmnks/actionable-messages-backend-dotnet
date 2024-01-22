using AdaptiveCards.Templating;

namespace ActionableMessagesBackend.Helpers
{
    public class CardHelper
    {
        public static string ExpandCard(string json)
        {
            string cardJsonString;
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(json);
            cardJsonString = template.Expand(json);

            return cardJsonString;
        }

        public static string TermsAndConditionsCard = @"
        {
          ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
          ""originator"": ""f3d86ba6-bf2c-4576-b8f6-684165dbe26b"",
          ""body"": [
            {
              ""id"": ""1"",
              ""type"": ""TextBlock"",
              ""size"": ""Medium"",
              ""weight"": ""Bolder"",
              ""text"": ""Terms and conditions""
            },
            {
              ""id"": ""2"",
              ""type"": ""TextBlock"",
              ""text"": ""Do you accept our terms and conditions?"",
              ""wrap"": true
            },
            {
              ""type"": ""ActionSet"",
              ""id"": ""actionId123"",
              ""actions"": [
                {
                  ""id"": ""3"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Accept"",
                  ""verb"": ""termsAccept"",
                  ""data"": {},
                  ""style"": ""positive""
                },
                {
                  ""id"": ""4"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Decline"",
                  ""verb"": ""termsDecline"",
                  ""data"": {}
                }
              ],
              ""autoInvokeAction"": {
                ""type"": ""Action.Execute"",
                ""verb"": ""test"",
                ""data"": {}
              }
            }
          ],
          ""refresh"": {
            ""action"": {
              ""type"": ""Action.Execute"",
              ""verb"": ""initialRefresh"",
              ""data"": {}
            }
          },
          ""type"": ""AdaptiveCard"",
          ""version"": ""1.4""
        }";

        public static string TermsAndConditionsCardAccept = @"
        {
          ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
          ""originator"": ""f3d86ba6-bf2c-4576-b8f6-684165dbe26b"",
          ""body"": [
            {
              ""id"": ""1"",
              ""type"": ""TextBlock"",
              ""size"": ""Medium"",
              ""weight"": ""Bolder"",
              ""text"": ""Terms and conditions""
            },
            {
              ""id"": ""2"",
              ""type"": ""TextBlock"",
              ""text"": ""Accept"",
              ""wrap"": true
            },
            {
              ""type"": ""ActionSet"",
              ""id"": ""actionId123"",
              ""isVisible"": false,
              ""actions"": [
                {
                  ""id"": ""3"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Accept"",
                  ""verb"": ""termsAccept"",
                  ""data"": {},
                  ""style"": ""positive""
                },
                {
                  ""id"": ""4"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Decline"",
                  ""verb"": ""termsDecline"",
                  ""data"": {}
                }
              ]
            }
          ],
          ""type"": ""AdaptiveCard"",
          ""version"": ""1.4""
        }";

        public static string TermsAndConditionsCardDecline = @"
        {
          ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
          ""originator"": ""f3d86ba6-bf2c-4576-b8f6-684165dbe26b"",
          ""body"": [
            {
              ""id"": ""1"",
              ""type"": ""TextBlock"",
              ""size"": ""Medium"",
              ""weight"": ""Bolder"",
              ""text"": ""Terms and conditions""
            },
            {
              ""id"": ""2"",
              ""type"": ""TextBlock"",
              ""text"": ""Decline"",
              ""wrap"": true
            },
            {
              ""type"": ""ActionSet"",
              ""isVisible"": false,
              ""id"": ""actionId123"",
              ""actions"": [
                {
                  ""id"": ""3"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Accept"",
                  ""verb"": ""termsAccept"",
                  ""data"": {},
                  ""style"": ""positive""
                },
                {
                  ""id"": ""4"",
                  ""isEnabled"": true,
                  ""type"": ""Action.Execute"",
                  ""title"": ""Decline"",
                  ""verb"": ""termsDecline"",
                  ""data"": {}
                }
              ]
            }
          ],
          ""type"": ""AdaptiveCard"",
          ""version"": ""1.4""
        }";
    }
}