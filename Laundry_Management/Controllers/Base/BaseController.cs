using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace Laundry_Management.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _request;

        public BaseController( IHttpContextAccessor request)
        {
            
            _request = request;
        }
        //verify Token
        // return userId
        public User CheckAuthen()
        {

            //HTTP request to provide information about the request
            string token = _request.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var decode = Encrypt.Base64Decode(token);

            AuthorizeToken auth = JsonConvert.DeserializeObject<AuthorizeToken>(decode);
            var current_time = DateTime.UtcNow;
            var user = new User();
            user.PhoneNumber = auth.Phone;
            user.CreateDate = current_time;
            if (current_time > auth.ExpireDate)
            {
                return null;
            }

            
            return user;
        }
    }
}
