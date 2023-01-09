using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry_Management.Controllers.Base;
using Laundry_Management.DTO.LocationDTO;
using System.Linq;
using Laundry_Management.Services;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private readonly LaundryContext _context;
        private readonly IHttpContextAccessor _request;
        private readonly ILocation _location;


        public LocationController(LaundryContext context, IHttpContextAccessor request, ILocation location) : base(request, context)
        {
            _context = context;
            _request = request;
            _location = location;
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
        public async Task<ResponseResult> Add(LocationAddDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }

            await _location.addDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }

        [HttpPut("Update")]
        public async Task<ResponseResult> Update(LocationUpdateDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            await _location.updateDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }

        [HttpDelete("Delete")]
        public async Task<ResponseResult> Delete(LocationDeleteDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            await _location.deleteDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }
    }
}
