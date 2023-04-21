using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser _user;
        private readonly LaundryContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(IUser user, LaundryContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ResponseResult> Register(RegisterUser model)
        {
            var data = await _user.Register(model);
            if (data == null) return new ResponseResult().ResponsFailure();
            
            return new ResponseResult().ResponseSuccess(data);
        }

        [HttpPost("login")]
        public async Task<ResponseResult> Login(LoginModel model)
        {
            var data = await _user.Login(model);
            if (data == null) return new ResponseResult().ResponsFailure();

            return new ResponseResult().ResponseSuccess(data);
        }

        [HttpPost("userRegistration")]
        public async Task<ResponseResult> UserRegistration(RegisterUser model)
        {
            var dbUser = _context.Users.Where(u => u.PhoneNumber == model.Phone).FirstOrDefault();
            if (dbUser != null) return new ResponseResult().ResponsFailure(null, "This Account already exist");

            var user = new User();
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            user.Salt = Guid.NewGuid().ToString();
            
            var passHash = Encrypt.hashPassword(model.Password + user.Salt);
            user.PassHash = passHash;
            _context.Users.Add(user);
            var data = await _context.SaveChangesAsync();
            if (data == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(data);
        }
    }
}
