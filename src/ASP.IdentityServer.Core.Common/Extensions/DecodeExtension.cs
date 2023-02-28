using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ASP.IdentityServer.Core.Common.Extensions
{
    public static class DecodeExtension
    {
        public static string Decode(this string value, Encoding encoding)
        {
            var bytes = WebEncoders.Base64UrlDecode(value);

            return encoding.GetString(bytes);
        }

        public static string DecodeTextBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
