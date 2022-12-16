using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO
{
    public class MachineDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string MachineName { get; set; }
        public sbyte? MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }
    }
}
