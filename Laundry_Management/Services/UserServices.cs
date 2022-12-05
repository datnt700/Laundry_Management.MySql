using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laundry_Management.Services
{
    public interface IUser
    {
        Task<UserDTO> Register(RegisterUser model);
    }

    public class UserServices : IUser
    {
        private readonly IMachine _machine;
        private readonly LaundryContext _context;
        public UserServices(IMachine machine, LaundryContext context)
        {
            this._machine = machine;
            this._context = context;
        }

        public async Task<UserDTO> Register(RegisterUser model)
        {
            var dbUser = _context.Users.Where(u => u.PhoneNumber  == model.Phone).FirstOrDefault();
            if (dbUser != null)
            {
                return null;
            }
            var user = new User();
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            user.Salt = Guid.NewGuid().ToString();// trả về đối tượng string;

            var passHash = Encrypt.EncodeAccount(model.Password + user.Salt);
            user.PassHash = passHash;
            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDTO();
            // kiểm tra số diện thoại đã đc tạo chưa
            //if (model.Phone == user.PhoneNumber) return null;
            //var user = new User();

            //user = new User
            //{
            //    PhoneNumber = model.Phone,
            //    Salt = Guid.NewGuid().ToString()// trả về đối tượng string
            //};

            //var passHash = Encrypt.EncodeAccount(model.Password + user.Salt);
            //user.PassHash = passHash;
            //_context.Add(user);
            //await _context.SaveChangesAsync();
            //// insert database
            //// return tài khoản đã được đăng kí
            //return Ok(user);
        }

        public async Task<UserDTO> Login(LoginModel model)
        {
            //kiểm tra tài khoản có tồn tại không
            var user = new User();
            if (user == null) return null;
            // lấy tài khoản đấy ra
            //kiểm tra mật khẩu có đúng chưa

            var passHash = Encrypt.EncodeAccount(model.Password + user.Salt);
            if (passHash != user.PassHash) return null;

            var token = new AuthorizeToken
            {
                Phone = model.Phone
            }.GenerateToken();

            var userResp = new UserDTO
            {
                Token = token
            };
            return userResp;


        }
    }
}
