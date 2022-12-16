using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Laundry_Management.Services
{
    public interface IUser
    {
        Task<UserDTO> Register(RegisterUser model);
        Task<UserDTO> Login(LoginModel model);
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
            var dbUser = _context.Users.FirstOrDefault(u => u.PhoneNumber == model.Phone);
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
            user.CreateDate = DateTime.Now;

            _context.Users.Add(user);
            var res = _context.SaveChanges();
            if (res == 0) return null;


            return new UserDTO
            {
                Token = new AuthorizeToken
                {
                    Phone = model.Phone
                }.GenerateToken()
            };
        }
        public async Task<UserDTO> Login(LoginModel model)
        {
            //kiểm tra tài khoản có tồn tại không

            // lấy tài khoản đấy ra
            //kiểm tra mật khẩu có đúng chưa
            var dbUser = _context.Users.FirstOrDefault(u => u.PhoneNumber == model.Phone);
            if (dbUser == null)
            {
                return null;
            }
            var passHash = Encrypt.EncodeAccount(model.Password + dbUser.Salt);
            if (passHash != dbUser.PassHash) return null;

            return new UserDTO
            {
                Token = new AuthorizeToken
                {
                    Phone = model.Phone
                }.GenerateToken(),

            };
        }


    }
}
