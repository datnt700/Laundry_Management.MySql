using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Laundry_Management.Common
{
    public static class Encrypt
    {
        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }

        //}
        //private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        // mã hóa từ binary sang string
        public static string EncodeAccount(string txt)
        {
            // MD5, base64,
            //123456 -> abcde
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(txt));
            return txt;
        }
    }
}
