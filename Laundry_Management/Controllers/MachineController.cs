using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
using Laundry_Management.DTO.Request;
using Laundry_Management.DTO;
using Laundry_Management.Models;
using Laundry_Management.Procedures;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : BaseController
    {
        private readonly LaundryContext _context;
        private readonly LaundryContextProcedures _contextProcedures;
        private readonly IMachine _machineService;
        private readonly IHttpContextAccessor _request;

        public MachineController(LaundryContext context, LaundryContextProcedures contextProcedures,IMachine machine, IHttpContextAccessor request) : base(request)
        {
            _context = context;
            _contextProcedures = contextProcedures;
            _machineService = machine;
            _request = request;
        }

        [HttpGet("GetByFilter")]
        public async Task<Paginate> GetByFilter([FromQuery] FitlerUserModel model)
        {
            var validFilter = new FitlerUserModel(model.PageIndex, model.PageSize);

            var queryUser = _context.Machines.AsQueryable();

            var lsMachine = queryUser.Skip((validFilter.PageIndex - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            return new Paginate(lsMachine, validFilter.PageIndex, validFilter.PageSize);
        }


        [HttpGet]
        [Route("GetToken")]
        public async Task<ResponseResult> GetToken()
        {

            var token = new AuthorizeToken
            {
                Phone = "0123456789",
                ExpireDate = DateTime.Now.AddDays(5)
            }.GenerateToken();
            return new ResponseResult().ResponseSuccess(token);
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<ResponseResult> GetById(int machineId, int userId)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _machineService.GetById(machineId,userId);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(machine);
        }

        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _context.Machines.FromSqlRaw("Select * from Machine").ToListAsync();
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(machine);
        }

        //[HttpGet("{id}")]
        //public async Task<ResponseResult> GetMachineById(int id)
        //{
        //    var machine = await _contextProcedures.sp_GetMachineById.FromSqlRaw("CALL sp_GetMachineById({0})", id).ToListAsync();
        //    if (machine == null) return new ResponseResult().ResponsFailure(null, "");
        //    return new ResponseResult().ResponseSuccess(machine);
        //}

        [HttpPost]
        public async Task<ResponseResult> Add(MachineDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            if (String.IsNullOrEmpty(dto.MachineName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }

            Machine machine = new Machine();

            machine.MachineName = dto.MachineName;
            machine.MachineType = dto.MachineType;
            machine.Branch = dto.Branch;
            machine.Size = dto.Size;
            machine.Location.LocationName = dto.LocationName;
            
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return new ResponseResult().ResponseSuccess(machine);

        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> UpdateMachine(int id, MachineDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            if (String.IsNullOrEmpty(dto.MachineName))
            {
                return new ResponseResult().ResponsFailure(null, "");
            }
            var dbmachine = _context.Machines.Where(m => m.MachineId == id).SingleOrDefault();
            if (dbmachine == null)
                return new ResponseResult().ResponsFailure(null, "");
          
            dbmachine.MachineName = dto.MachineName;
            dbmachine.MachineType= dto.MachineType;
            dbmachine.Branch = dto.Branch;
            dbmachine.Size = dto.Size;
            dbmachine.Location.LocationName = dto.LocationName;
            
            
            await _context.SaveChangesAsync();
            return new ResponseResult().ResponseSuccess(dbmachine);
        }

        //[HttpGet("{MachineId}/{MachineName}")]
        //public async Task<IEnumerable<sp_UpdateMachine>> UpdateMachineName(Machine machine)
        //{
        //    var result = await _contextProcedures.sp_UpdateMachine.FromSqlRaw(" call sp_UpdateMachine ({0}), ({1})", machine.MachineId, machine.MachineName).ToListAsync();
        //    return result;
        //}

        [HttpDelete("{id}")]
        public async Task<ResponseResult> DeleteMachine(int id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
                return new ResponseResult().ResponsFailure(null, "");
            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return new ResponseResult().ResponseSuccess(machine);

        }

        [HttpGet("GetLocationFromMachineName")]
        public async Task<Machine?> GetLocationFromMachineName(string? machineName)
        {
            var machine = await _context.Machines
                .Include(l => l.Location)
                .Where(l => l.MachineName == machineName)
                .FirstOrDefaultAsync();
            return machine;
        }
    }
}
