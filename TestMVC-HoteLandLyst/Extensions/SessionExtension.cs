using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Extensions
{
    public static class SessionExtension
    {
        /// <summary>
        /// Serialize object as Json string
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="key">The key you want ot input at</param>
        /// <param name="value">The object you want to store in the session</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
