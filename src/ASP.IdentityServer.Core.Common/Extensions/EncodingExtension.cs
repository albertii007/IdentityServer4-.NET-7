using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ASP.IdentityServer.Core.Common.Extensions
{
    public static class EncodingExtension
    {
        public static string Encode(this string value, Encoding encoding)
        {
            var bytes = encoding.GetBytes(value);

            return WebEncoders.Base64UrlEncode(bytes);
        }


        public static string EncodeTextBase64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
