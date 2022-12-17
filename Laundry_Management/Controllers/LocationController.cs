using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry_Management.Controllers.Base;
using Laundry_Management.DTO;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly LaundryContext _context;
        private readonly IHttpContextAccessor _request;


        public LocationController(LaundryContext context, IHttpContextAccessor request) : base(request)
        {
            _context = context;
            _request = request;
        }

        [HttpGet]
        public async Task<ResponseResult> GetAll()
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var location = await _context.Locations.ToListAsync();
            if (location == null) return new ResponseResult().ResponsFailure(null, "");


            return new ResponseResult().ResponseSuccess(location);
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> GetById(int id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var location = await _context.Locations.FindAsync(id);
            if (location == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(location);
        }

        [HttpPost]
        public async Task<ResponseResult> Add(LocationDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }

            if (String.IsNullOrEmpty(dto.LocationName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }

            Location location = new Location()
            {
                LocationName = dto.LocationName,
                Coordinates = dto.Coordinates
            };

            if (location == null) return new ResponseResult().ResponsFailure(null, "");
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(location);
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> Update( int id, LocationDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            if (String.IsNullOrEmpty(dto.LocationName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }

            var dbLocation = _context.Locations.Where(l => l.LocationId == id).SingleOrDefault();
            if (dbLocation == null)
            {
                return new ResponseResult().ResponsFailure(null, "");
            }
            dbLocation.LocationName = dto.LocationName;
            dbLocation.Coordinates= dto.Coordinates;

            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(dbLocation);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseResult> Delete(int id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var location = await _context.Locations.FindAsync(id);
            if (User == null) return new ResponseResult().ResponsFailure(null, "");

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(location);
        }
    }
}
