using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.UserDTO
{
    public class UserDeleteDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
