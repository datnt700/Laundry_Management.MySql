using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.MachineDTO
{
    public class MachineAddDTO
    {
        [Required]
        public string MachineName { get; set; }
        public sbyte? MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }

    }
}
