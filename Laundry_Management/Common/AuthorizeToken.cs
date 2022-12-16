﻿using Newtonsoft.Json;
namespace Laundry_Management.Common
{
    public class AuthorizeToken
    {
        public string Phone { get; set; }
        public DateTime ExpireDate { get; set; }

        public string GenerateToken()
        {
            //JsonConvert.SerializeObject: convert object into Json
            return Encrypt.EncodeAccount(JsonConvert.SerializeObject(new AuthorizeToken
            {
                Phone = Phone,
                ExpireDate = DateTime.Now.AddDays(5)
            }));

        }
    }
}
