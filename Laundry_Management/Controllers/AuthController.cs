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

        public void Jsontodynamic()
        {
            dynamic fyn = JsonConvert.DeserializeObject("{\r\n        \"Token\": \"eyJQaG9uZSI6IjEyMzQxMjM0IiwiRXhwaXJlRGF0ZSI6IjIwMjMtMDEtMTlUMDc6MTg6MjAuOTk1MTYyOSswMTowMCJ9\",\r\n        \"UserName\": null,\r\n        \"Phone\": null,\r\n        \"Password\": null,\r\n        \"CreateDate\": \"0001-01-01T00:00:00\"\r\n    }");
        }

        [HttpPost("userRegistration")]
        public async Task<ResponseResult> UserRegistration(RegisterUser model)
        {
            var dbUser = _context.Users.Where(u => u.PhoneNumber == model.Phone).FirstOrDefault();
            if (dbUser != null) return new ResponseResult().ResponsFailure(null, "This Account already exist");

            var user = new User();
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            user.Salt = Guid.NewGuid().ToString();// trả về đối tượng string;
            
            var passHash = Encrypt.hashPassword(model.Password + user.Salt);
            user.PassHash = passHash;
            _context.Users.Add(user);
            var data = await _context.SaveChangesAsync();
            if (data == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(data);
        }


        private string CreateToken(LoginModel model)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Phone),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost("LOGINN")]
        public async Task<ActionResult<string>> LOGIN(LoginModel model)
        {
            var dbUser = _context.Users.Where(u => u.PhoneNumber == model.Phone).FirstOrDefault();
            if (dbUser == null)
            {
                return null;
            }
            //var passHash = Encrypt.EncodeAccount(model.Password + dbUser.Salt);
            //if (passHash != dbUser.PassHash) return null;
            string token = CreateToken(model);

            return Ok(token);
        }
    }
}
