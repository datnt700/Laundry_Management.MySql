using System.ComponentModel.DataAnnotations;

namespace Laundry_Management.DTO.LocationDTO
{
    public class LocationUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public string LocationName { get; set; }

        public string Coordinates { get; set; }
    }
}
