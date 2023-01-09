using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.UserDTO
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
