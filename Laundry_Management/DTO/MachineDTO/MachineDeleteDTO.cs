using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.MachineDTO
{
    public class MachineDeleteDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
