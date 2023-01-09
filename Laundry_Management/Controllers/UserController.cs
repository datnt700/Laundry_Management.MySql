using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.DTO.UserDTO;

using Laundry_Management.Models;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly LaundryContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUser _user;
        public UserController(LaundryContext context, IHttpContextAccessor httpContext, IUser _user) : base(httpContext, context)
        {
            _httpContext = httpContext;
            this._user = _user;
            _context = context;
        }

        [HttpGet("GetByFilter")]
        public async Task<Paginate> GetByFilter([FromQuery] FitlerUserModel model)
        {

            return new Paginate
            {
                data = await _context.Users.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).Select(e => new UserAddDTO
                {
                    Phone = e.PhoneNumber,
                    UserName = e.UserName,
                }).ToListAsync(),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> GetUserById(int id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var user = await _context.Users.FindAsync(id);
            if (user == null) return new ResponseResult().ResponsFailure(null, "");


            return new ResponseResult().ResponseSuccess(user);
        }

        [HttpGet("GetAll")]
        public async Task<ResponseResult> GetAll()
        {
             var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var user = await _context.Users.ToListAsync();
            if (user == null) return new ResponseResult().ResponsFailure(null, "");


            return new ResponseResult().ResponseSuccess(user);
        }

        [HttpPost("AddUser")]
        public async Task<ResponseResult> AddUser([FromBody] UserAddDTO user)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }

            var userRegister = await _user.Register(new RegisterUser
            {
                Phone = user.Phone,
                Password = user.Password,
                UserName = user.UserName
            });

  
            if (userRegister == null) return new ResponseResult().ResponsFailure();
            return new ResponseResult().ResponseSuccess(userRegister);
        }


        [HttpPut("Update")]
        public async Task<ResponseResult> Update(UserUpdateDTO user)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            await _user.UpdateDTO(user);
            return new ResponseResult().ResponseSuccess(user);
        }


        [HttpDelete("Delete")]
        public async Task<ResponseResult> Delete(UserDeleteDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            await _user.DeleteDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }

        [HttpGet("GetByName")]
        public async Task<User> GetByName(string name)
        {
            return await _user.GetByName(name);

        }
    }
}
