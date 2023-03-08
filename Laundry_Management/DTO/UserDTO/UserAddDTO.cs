using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.UserDTO
{
    public class UserAddDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
    }


}
