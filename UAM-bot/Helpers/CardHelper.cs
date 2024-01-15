using AdaptiveCards;
using AdaptiveCards.Templating;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.IO;
using UAM_bot.Models;

namespace UAM_bot.Helpers
{
    public class CardHelper
    {
        public static Attachment GetResponseAttachment(string[] filepath, out string cardJsonString)
        {
            var adaptiveCardJson = File.ReadAllText(Path.Combine(filepath));
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(adaptiveCardJson);
            cardJsonString = template.Expand(adaptiveCardJson);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };

            return adaptiveCardAttachment;
        }

        public static string GetJson(string[] filepath)
        {
            string cardJsonString;
            var adaptiveCardJson = File.ReadAllText(Path.Combine(filepath));
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(adaptiveCardJson);
            cardJsonString = template.Expand(adaptiveCardJson);

            return cardJsonString;
        }
    }
}