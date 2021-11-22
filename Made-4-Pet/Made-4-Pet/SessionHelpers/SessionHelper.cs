using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.SessionHelpers
{
    public static class SessionHelper
    {
        //Seta um objeto como json como variável na sessão
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //Pega um objeto json da variável da sessão, deserializando ele.
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
