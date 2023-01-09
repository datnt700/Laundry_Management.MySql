using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO.LocationDTO;
using Laundry_Management.Models;

namespace Laundry_Management.Services
{
    public interface ILocation
    {
        Task<LocationUpdateDTO> updateDTO(LocationUpdateDTO dto);

        Task<LocationAddDTO> addDTO(LocationAddDTO dto);

        Task<LocationDeleteDTO> deleteDTO(LocationDeleteDTO dto);
    }
    public class LocationService : ILocation
    {
        private readonly LaundryContext _context;

        public LocationService(LaundryContext context)
        {
            _context = context;
        }

        public async Task<LocationAddDTO> addDTO (LocationAddDTO dto)
        {
            Location location = new Location()
            {
                LocationName = dto.LocationName,
                Coordinates = dto.Coordinates
            };

            if (location == null) return null;
            _context.Locations.Add(location);
            int res = await _context.SaveChangesAsync();
            if (res == 0) return null;
            return new LocationAddDTO();
        }
        public async Task<LocationUpdateDTO> updateDTO(LocationUpdateDTO dto)
        {
            var dbLocation = _context.Locations.FirstOrDefault(l => l.LocationId == dto.Id);
            if (dbLocation == null) return null;

            dbLocation.LocationName = dto.LocationName;
            dbLocation.Coordinates = dto.Coordinates;
            int res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new LocationUpdateDTO();
        }

        public async Task<LocationDeleteDTO> deleteDTO(LocationDeleteDTO dto)
        {
            var dbLocation = _context.Locations.FirstOrDefault(l => l.LocationId == dto.Id);
            if (dbLocation == null) return null;

            _context.Locations.Remove(dbLocation);
            int res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new LocationDeleteDTO();
        }

    }
}
