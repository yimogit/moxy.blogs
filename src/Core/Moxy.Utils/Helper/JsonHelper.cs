using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Utils
{
    public class JsonHelper
    {
        public static string Serialize(object o)
        {
            var setting = new JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(o, setting);
        }

        public static T Deserialize<T>(string input)
        {
            if (string.IsNullOrEmpty(input))
                return default(T);
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static dynamic Deserialize(string input)
        {
            return JsonConvert.DeserializeObject(input);
        }
    }
}
