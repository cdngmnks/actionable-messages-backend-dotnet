using AdaptiveCards.Templating;
using System.IO;

namespace UAM_bot.Helpers
{
    public class CardHelper
    {
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