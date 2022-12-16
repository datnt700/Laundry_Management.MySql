using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
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
        public UserController(LaundryContext context, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        [HttpGet]
        public async Task<Paginate> GetByFilter([FromQuery] FitlerUserModel model)
        {
            var validFilter = new FitlerUserModel(model.PageIndex, model.PageSize);
            //var lsUser = await _context.Users
            //.Skip((validFilter.PageIndex - 1) * validFilter.PageSize)
            //.Take(validFilter.PageSize)
            //.ToListAsync();

            var queryUser = _context.Users.AsQueryable();

            var lsUser = queryUser.Skip((validFilter.PageIndex - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            return new Paginate(lsUser, validFilter.PageIndex, validFilter.PageSize);
            //return Ok(lsUser);
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

        [HttpPost]
        public async Task<ResponseResult> AddUser(UserDTO user)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            if (String.IsNullOrEmpty(user.UserName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }
            User dbuser = new User();
            dbuser.UserName = user.UserName;    
            dbuser.PhoneNumber = user.Phone;
            dbuser.CreateDate = DateTime.Now;

            if (user == null) return new ResponseResult().ResponsFailure(null, "");
            _context.Users.Add(dbuser);
            await _context.SaveChangesAsync(); 
            return new ResponseResult().ResponseSuccess(user);
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> UpdateUser(int id, UserDTO user)
        {
            
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            if (String.IsNullOrWhiteSpace(user.UserName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }

            var dbUser = _context.Users.Where(u => u.UserId == id).SingleOrDefault();
            if (dbUser == null) return new ResponseResult().ResponsFailure(null, "Invalid User Id");
            
            dbUser.UserName = user.UserName;
            dbUser.PhoneNumber = user.Phone;

            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(user);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseResult> Delete(int id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ResponseResult().ResponsFailure(null, "");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(user);
        }
    }
}
