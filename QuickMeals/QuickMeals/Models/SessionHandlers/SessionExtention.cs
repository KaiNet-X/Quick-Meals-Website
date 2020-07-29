using System;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Http
{
    public static class SessionExtention
    {
        //simplifies passing an object into session
        public static void SetObject<T>(this ISession session, string key, T Object)
        {
            session.SetString(key, JsonConvert.SerializeObject(Object));
        }

        //simplifies getting an object from session
        public static T GetObject<T>(this ISession session, string key)
        {
            if (!session.Keys.Contains(key)) return default(T);
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}

