using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.DTO.UserDTO;

using Laundry_Management.Models;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ResponseResult> GetAll([FromQuery] FitlerModel model)
        {
            // var check = CheckAuthen();
            //if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var user = await _user.GetAll(model);
            if (model == null) return new ResponseResult().ResponsFailure(null, "");
            var response = new ResponseResult().ResponseSuccess(user);
            return response;
        }

        [HttpPost("AddUser")]
        public async Task<ResponseResult> AddUser([FromBody] UserAddDTO user)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }

           await _user.AddDTO(user);
            if (user == null) return new ResponseResult().ResponsFailure();
            return new ResponseResult().ResponseSuccess(user);
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
        public async Task<ResponseResult> Delete(deleteId id)
        {
            
            var user = await _user.DeleteDTO(id.Id);
            return new ResponseResult().ResponseSuccess(id.Id);
        }

        [HttpGet("GetByName")]
        public async Task<User> GetByName(string name)
        {
            return await _user.GetByName(name);

        }

        public class deleteId
        {
            public int Id { get; set; }
        }
    }
}
