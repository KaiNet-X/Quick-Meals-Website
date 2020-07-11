using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Http
{
    public static class SessionExtention
    {
        public static void SetObject<T>(this ISession session, string key, T Object)
        {
            session.SetString(key, JsonConvert.SerializeObject(Object));
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            if (!session.Keys.Contains(key)) return default(T);
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}

