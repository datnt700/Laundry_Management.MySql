using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json.Converters;

namespace Laundry_Management.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _request;
        private readonly LaundryContext _context;


        public BaseController(IHttpContextAccessor request, LaundryContext context)
        {

            _request = request;
            _context = context;
        }
        //verify Token
        // return userId
        public User CheckAuthen()
        {
            try
            {
                //HTTP request to provide information about the request
                string token = _request.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(token))
                    return null;

                var decode = Encrypt.Base64Decode(token);
                AuthorizeToken auth = JsonConvert.DeserializeObject<AuthorizeToken>(decode);
                if (auth == null) return null;

                var user =  (from p in _context.Users where p.PhoneNumber == auth.Phone select p). FirstOrDefault();

                if (user != null && DateTime.Now < auth.ExpireDate) return user;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static string SerializeWithStringEnum(object obj)
        {
            var converter = new StringEnumConverter();
            return JsonConvert.SerializeObject(obj, converter);
        }
    }
}
