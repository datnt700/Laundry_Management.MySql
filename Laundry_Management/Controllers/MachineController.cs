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
using System;
using Laundry_Management.DTO.UserDTO;
using Laundry_Management.DTO;
using System.Reflection.PortableExecutable;

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



      


        [HttpGet("GetByFilter")]
        public async Task<ResponseResult> GetByFilter([FromQuery] FitlerModel model)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _machineService.GetAll(model);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            var response = new ResponseResult().ResponseSuccess(machine);
            return response;
        }

        //[HttpGet("search")]
        //public ResponseResult Search([FromQuery] FitlerModel model)
        //{
        //    //var check = CheckAuthen();
        //    //if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
        //    var machine = _machineService.Search(model);
        //    if (machine == null) return null;
        //    var response = new ResponseResult().ResponseSuccess(machine);
        //    return response;
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
        [Route("{id:int}")]
        public async Task<ResponseResult> GetById(int id)
        {
            //var check = CheckAuthen();
            //if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _machineService.GetById(id);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(machine);
        }

        [HttpGet]
        [Route("GetList")]
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

        [HttpPost("AddMachine")]
        public async Task<ResponseResult> Add(MachineAddDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "Machine not exist"); }
            var machine = await _machineService.AddDTO(dto);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(dto);
        }

        [HttpPut("UpdateMachine")]
        public async Task<ResponseResult> UpdateMachine(MachineUpdateDTO dto)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "Machine not exist"); }
            var machine = await _machineService.UpdateDTO(dto);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");

            return new ResponseResult().ResponseSuccess(dto);
        }

     

        [HttpDelete("delete")]
        public async Task<ResponseResult> DeleteMachine(IdInput id)
        {
            var check = CheckAuthen();
            if (check == null) { return new ResponseResult().ResponsFailure(null, "User not exist"); }
            var machine = await _machineService.DeleteDTO(id.Id);
            if (machine == null) return new ResponseResult().ResponsFailure(null, "");
            return new ResponseResult().ResponseSuccess(id.Id);

        }

        //[HttpGet("GetByName")]
        //public async Task<Machine?> GetByName(string? machineName)
        //{
        //    return await _machineService.GetByName(machineName);
        //}

        //[HttpGet("GetByLocation")]
        //public async Task<List<Machine>> GetByLocation(int locationId)
        //{
        //    return await _machineService.GetByLocation(locationId);
        //}

        //[HttpGet("GetByStatus")]
        //public async Task<List<Machine>> GetByState(status status)
        //{
        //    return await _machineService.GetByStatus(status);
        //}
        public class ApiInputModel
        {
            public status Status { get; set; }
        }

        public class IdInput
        {
            public int Id { get; set; }
        }
        
    }
}
