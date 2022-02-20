using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCoreSession.Helpers
{
    public class HelperSession
    {
        public static string SerializarObjeto(Object obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            return data;
        }

        public static T DeserializarObjeto<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
