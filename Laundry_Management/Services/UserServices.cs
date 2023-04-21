using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.UserDTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Laundry_Management.Services
{
    public interface IUser
    {
        Task<UserAddDTO> Register(RegisterUser model);
        Task<UserAddDTO> Login(LoginModel model);

        Task<UserUpdateDTO> UpdateDTO(UserUpdateDTO dto);

        Task<UserDeleteDTO> DeleteDTO(int id);

        Task<User> GetByName(string name);

        Task<UserAddDTO> AddDTO(UserAddDTO dto);

        Task<FilterUser> GetAll([FromQuery] FitlerModel model);
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


        public async Task<UserAddDTO> Register(RegisterUser model)
        {

            var user = new User();
            user.PhoneNumber = model.Phone;
            user.UserName = model.UserName;
            user.Salt = Guid.NewGuid().ToString();

            var passHash = Encrypt.EncodeAccount(model.Password + user.Salt);
            user.PassHash = passHash;
            user.CreateDate = DateTime.Now;

            _context.Users.Add(user);
            var res = _context.SaveChanges();
            if (res == 0) return null;


            return new UserAddDTO
            {
                Token = new AuthorizeToken
                {
                    Phone = model.Phone
                }.GenerateToken()
            };
        }
        public async Task<UserAddDTO> AddDTO(UserAddDTO user)
        {
            var userRegister = await Register(new RegisterUser
            {
                Phone = user.Phone,
                Password = user.Password,
                UserName = user.UserName
            });
            return new UserAddDTO();
        }
        public async Task<UserAddDTO> Login(LoginModel model)
        {
    
            var dbUser = _context.Users.FirstOrDefault(u => u.PhoneNumber == model.Phone);
            if (dbUser == null)
            {
                return null;
            }
            var passHash = Encrypt.EncodeAccount(model.Password + dbUser.Salt);
            if (passHash != dbUser.PassHash) return null;

            return new UserAddDTO
            {
                Token = new AuthorizeToken
                {
                    Phone = model.Phone
                }.GenerateToken(),
            };
        }

        public async Task<UserUpdateDTO> UpdateDTO(UserUpdateDTO dto)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.Id);
            if (dbUser == null) return null;

            dbUser.UserName = dto.UserName;
            dbUser.PhoneNumber = dto.Phone;
            int res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new UserUpdateDTO();

        }

        public async Task<UserDeleteDTO> DeleteDTO(int id)
        {
            try
            {
                _context.Locations.RemoveRange(_context.Locations.ToList());
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (user == null) return null;
                _context.Users.Remove(user);
                int res = await _context.SaveChangesAsync();
                if (res < 1) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.Message);
            }
                return new UserDeleteDTO();
        }

        public async Task<User> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            return user;

        }

        public async Task<FilterUser> GetAll([FromQuery] FitlerModel model)
        {
            var query = _context.Users.Where(c => (!string.IsNullOrEmpty(model.search)) ? c.UserName.Contains(model.search) : true);

            return new FilterUser
            {
                LisData = await query.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).Select(e => new UserAddDTO
                {
                    Id = e.UserId,
                    UserName = e.UserName,
                    Phone = e.PhoneNumber,

                }).ToListAsync(),
                TotalCount = await query.CountAsync(),

            };
        }
    }
    public class FilterUser : Paginate
    {
        public List<UserAddDTO> LisData { get; set; }
    }
}
