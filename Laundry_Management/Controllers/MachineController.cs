using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
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
        public MachineController(LaundryContext context, LaundryContextProcedures contextProcedures,IMachine machine, IHttpContextAccessor request) : base(request)
        {
            _context = context;
            _contextProcedures = contextProcedures;
            _machineService = machine;
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
        public async Task<ResponseResult> GetById(int machineId)
        {
            var check = base.CheckAuthen(out int userId);

            var machine = await _machineService.GetById(machineId,userId);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(machine);
        }

        [HttpGet]
        public async Task<ActionResult<Machine>> Get()
        {
            var machines = await _context.Machines.FromSqlRaw("Select * from Machine").ToListAsync();
            return Ok(machines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetMachineById(int id)
        {
            var machine = await _contextProcedures.sp_GetMachineById.FromSqlRaw("CALL sp_GetMachineById({0})", id).ToListAsync();
            return Ok(machine);
        }

        [HttpPost]
        public async Task<ActionResult<Machine>> PostMachine(Machine machine)
        {
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return Ok(await _context.Machines.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<Machine>> UpdateMachine(Machine machine)
        {
            var dbmachine = await _context.Machines.FindAsync(machine.MachineId);
            if (dbmachine == null)
                return BadRequest("Machine not found");
            dbmachine.MachineId = machine.MachineId;
            dbmachine.MachineName = machine.MachineName;
            dbmachine.MachineType= machine.MachineType;
            dbmachine.Branch = machine.Branch;
            dbmachine.Size = machine.Size;
            dbmachine.Location = machine.Location;
            dbmachine.MachineHistories = machine.MachineHistories;
            dbmachine.LocationId = machine.LocationId;
            dbmachine.MachineModes = machine.MachineModes;
            dbmachine.IsActive = machine.IsActive;
            dbmachine.Status = machine.Status;
            
            await _context.SaveChangesAsync();
            return Ok(dbmachine);
        }

        [HttpGet("{MachineId}/{MachineName}")]
        public async Task<IEnumerable<sp_UpdateMachine>> UpdateMachineName(Machine machine)
        {
            var result = await _contextProcedures.sp_UpdateMachine.FromSqlRaw(" call sp_UpdateMachine ({0}), ({1})", machine.MachineId, machine.MachineName).ToListAsync();
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Machine>> DeleteMachine(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
                return BadRequest("Machine not found");
            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return Ok(await _context.Machines.ToListAsync());

        }
    }
}
