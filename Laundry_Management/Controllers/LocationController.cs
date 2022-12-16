using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry_Management.Controllers.Base;

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
        public async Task<ResponseResult> Add(Location location)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            _context.Locations.Add(location);
            if (location == null) return new ResponseResult().ResponsFailure(null, "");
            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(location);
        }

        [HttpPut]
        public async Task<ResponseResult> Update(Location location)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var dbLocation = await _context.Locations.FindAsync(location.LocationId);
            if (dbLocation == null)
            {
                return new ResponseResult().ResponsFailure(null, "");
            }
            dbLocation.LocationId = location.LocationId;
            dbLocation.LocationName = location.LocationName;
            dbLocation.Coordinates= location.Coordinates;
            dbLocation.IsActive= location.IsActive;
            dbLocation.UserIdHost= location.UserIdHost;

            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(location);
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
