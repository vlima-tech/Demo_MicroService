
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Praticis.Framework.Tools.Helpers
{
    public static class KeyBindHelper
    {
        public static IDictionary<string, string> ExtractKeyBinds<T>(T @object) where T : class
        {
            string key, value;
            var dict = new Dictionary<string, string>();

            foreach (var prop in @object.GetType().GetProperties())
            {
                if (prop.PropertyType.IsPublic && !prop.PropertyType.Namespace.ToLower().Contains("system"))
                {
                    ExtractKeyBinds(prop.GetValue(@object))
                        .ToList()
                        .ForEach(k => dict.Add($"{prop.Name}_{k.Key}", k.Value));
                    continue;
                }

                /*
                var key = prop.Name
                    .Aggregate(new StringBuilder(), (accumlator, propValue) =>
                    {
                        if (accumlator.Length > 0 && propValue.ToString() == propValue.ToString().ToUpper())
                            return accumlator.Append("_" + propValue);

                        return accumlator.Append(propValue);
                    })
                    .ToString();
                */

                //key = "{" + key + "}";
                key = "{" + prop.Name + "}";

                value = prop.GetValue(@object)?.ToString() ?? string.Empty;
                
                if (!dict.ContainsKey(key))
                    dict.Add(key, value);
            }

            return dict;
        }

    }
}