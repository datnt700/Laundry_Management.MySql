using Laundry_Management.Common;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;
        public AuthController(IUser user)
        {
            _user = user;
        }

        [HttpPost("register")]
        public async Task<ResponseResult> Register(RegisterUser model)
        {
            var data = _user.Register(model);
            if (data == null) return new ResponseResult().ResponsFailure();
            
            return new ResponseResult().ResponseSuccess(data);
        }

    }
}
