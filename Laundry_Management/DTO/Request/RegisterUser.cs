using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.Request
{
    public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required (ErrorMessage = "Password is mandatory field")]
        public string Password
        {
            get; set;
        }
    }
}
