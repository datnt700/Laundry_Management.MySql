using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.MachineDTO
{
    public class MachineUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MachineName { get; set; }
        public sbyte? MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }

        
    }
}
