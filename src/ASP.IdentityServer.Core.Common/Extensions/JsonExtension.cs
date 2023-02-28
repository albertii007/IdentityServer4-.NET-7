using Newtonsoft.Json;

namespace ASP.IdentityServer.Core.Common.Extensions
{
    public static class JsonExtension
    {
        public static T Deserialize<T>(this string value) where T : class
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string Serialize<T>(this T value) where T : class
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
