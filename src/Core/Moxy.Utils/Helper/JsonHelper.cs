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
            return JsonConvert.SerializeObject(o);
        }

        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static dynamic Deserialize(string input)
        {
            return JsonConvert.DeserializeObject(input);
        }
    }
}
