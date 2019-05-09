using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TestsES5.Models;

namespace TestsES5.Extensions
{

    public static class SessionExtensions
    {
               public static void SetQuestionStorageToSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetQuestionStorageFromSession<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}