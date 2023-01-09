using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.LocationDTO
{
    public class LocationDeleteDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
