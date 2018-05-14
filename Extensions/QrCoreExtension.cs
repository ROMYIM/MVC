using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MVC.MiddleWares;
using Newtonsoft.Json;

namespace MVC.Extensions
{
    public static class QrCoreExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            string jsonString = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonString);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var jsonString = session.GetString(key);
            return string.IsNullOrEmpty(jsonString) ? default(T) : JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleWare>();
        }
    }
}