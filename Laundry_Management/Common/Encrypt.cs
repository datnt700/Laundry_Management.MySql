using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Laundry_Management.Common
{
    public static class Encrypt
    {
        
        public static string Base64Decode(string base64EncodedData)
        {
            //base64EncodedData.PadRight(base64EncodedData.Length + (base64EncodedData.Length * 3) % 4, '=');
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        // mã hóa từ binary sang string
        public static string EncodeAccount(string txt)
        {
            // MD5, base64,
            //123456 -> abcde
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(txt));
        }

        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByArray = Encoding.Default.GetBytes(password);
            var hashPassword = sha.ComputeHash(asByArray);
            return Convert.ToBase64String(hashPassword);
        }

    }
}
