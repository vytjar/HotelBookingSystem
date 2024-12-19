using System.Security.Cryptography;

namespace HotelManagementSystem.Services.Utils
{
    public static class StringExtensions
    {
        public static string ToSha256(this string value)
        {
            return Convert.ToBase64String(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(value)));
        }
    }
}
