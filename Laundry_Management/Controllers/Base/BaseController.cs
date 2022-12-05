using Laundry_Management.Common;
using Laundry_Management.DTO;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace Laundry_Management.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _request;

        public BaseController(IHttpContextAccessor _request)
        {
            this._request = _request;
        }
        //verify Token
        public bool CheckAuthen(out int userId)
        {
            userId = 0;
            
            string token = _request.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var decode = Encrypt.Base64Decode(token);

            AuthorizeToken auth = JsonConvert.DeserializeObject<AuthorizeToken>(decode);


            //userId = token.

            // nếu k tồn tại hoặc token = null => lỗi
            // có tồn tại giải mã token => phone + hết hạn
            //check xem token hết hạn chưa
            // lấy người dùng bằng sô didenj thoại
            //userId = user.userId
            return true;
        }
    }
}
