using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO
{
    public class UserDTO
    {
        public string Token { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
