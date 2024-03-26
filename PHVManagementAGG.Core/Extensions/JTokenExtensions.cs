using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHVManagementAGG.Core.Extensions
{
    public static class JTokenExtensions
    {
        public static void UpdatePropertyValue(this JToken token, string propertyName, object newValue)
        {
            // Find the property by name using SelectToken
            JToken property = token.SelectToken(propertyName);

            // If the property exists, update its value
            if (property != null)
            {
                property.Replace(new JValue(newValue));
            }
        }

        public static void RemovePropertiesByRegex(this JToken token, string regexPattern)
        {
            if (token.Type == JTokenType.Object)
            {
                var propertiesToRemove = token.Children<JProperty>()
                    .Where(p => Regex.IsMatch(p.Name, regexPattern, RegexOptions.IgnoreCase))
                    .ToList();

                foreach (var propertyToRemove in propertiesToRemove)
                {
                    propertyToRemove.Remove();
                }

                foreach (var child in token.Children())
                {
                    RemovePropertiesByRegex(child, regexPattern);
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var child in token.Children())
                {
                    RemovePropertiesByRegex(child, regexPattern);
                }
            }
        }
    }
}
