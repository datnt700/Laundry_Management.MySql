using Laundry_Management.Common;
using Laundry_Management.Controllers.Base;
using Laundry_Management.Data;
using Laundry_Management.DTO.Request;
using Laundry_Management.Models;
using Laundry_Management.Procedures;
using Laundry_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laundry_Management.DTO.MachineDTO;
using static Laundry_Management.Models.Machine;
using System.Text.Json.Serialization;

namespace Laundry_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : BaseController
    {
        private readonly LaundryContextProcedures _contextProcedures;
        private readonly IMachine _machineService;
        private readonly IHttpContextAccessor _request;
        private readonly LaundryContext _context;

        public MachineController(LaundryContextProcedures contextProcedures,IMachine machine, IHttpContextAccessor request, LaundryContext context) : base(request, context)
        {
            _request = request;
            _contextProcedures = contextProcedures;
            _machineService = machine;
            _context = context;
        }

      

        //[HttpGet("GetByFilter")]
        //public async Task<Paginate> GetByFilter([FromQuery] FitlerUserModel model)
        //{
        //    var validFilter = new FitlerUserModel(model.PageIndex, model.PageSize);

        //    var queryUser = _context.Machines.AsQueryable();

        //    var lsMachine = queryUser.Skip((validFilter.PageIndex - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
        //    return new Paginate(lsMachine, validFilter.PageIndex, validFilter.PageSize);
        //}


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
        [Route("GetList")]
        public async Task<ResponseResult> Get()
        {
            //var check = CheckAuthen();
            //if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
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
        public async Task<ResponseResult> Add(MachineAddDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "Machine not exist"); }
            await _machineService.AddDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }

        [HttpPut("UpdateMachine")]
        public async Task<ResponseResult> UpdateMachine(MachineUpdateDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "Machine not exist"); }
            await _machineService.UpdateDTO(dto);
            return new ResponseResult().ResponseSuccess(dto);
        }

        //[HttpGet("{MachineId}/{MachineName}")]
        //public async Task<IEnumerable<sp_UpdateMachine>> UpdateMachineName(Machine machine)
        //{
        //    var result = await _contextProcedures.sp_UpdateMachine.FromSqlRaw(" call sp_UpdateMachine ({0}), ({1})", machine.MachineId, machine.MachineName).ToListAsync();
        //    return result;
        //}

        [HttpDelete("DeleteMachine")]
        public async Task<ResponseResult> DeleteMachine(MachineDeleteDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            await _machineService.DeleteDTO(dto);

            return new ResponseResult().ResponseSuccess(dto);

        }

        [HttpGet("GetByName")]
        public async Task<Machine?> GetByName(string? machineName)
        {
            return await _machineService.GetByName(machineName);
        }

        [HttpGet("GetByLocation")]
        public async Task<List<Machine>> GetByLocation(int locationId)
        {
            return await _machineService.GetByLocation(locationId);
        }

        [HttpGet("GetByStatus")]
        public async Task<List<Machine>> GetByState(status status)
        {
            return await _machineService.GetByStatus(status);
        }
    }
}
