using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO;
using Laundry_Management.DTO.MachineDTO;
using Laundry_Management.DTO.Request;
using Laundry_Management.DTO.UserDTO;
using Laundry_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Laundry_Management.Models.Machine;
using static Laundry_Management.Services.MachineServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Laundry_Management.Services
{
    public interface IMachine
    {
        Task<Machine> GetById(int machineId);
        Task<MachineUpdateDTO> UpdateDTO(MachineUpdateDTO dto);

        Task<MachineAddDTO> AddDTO(MachineAddDTO dto);

        Task<MachineDeleteDTO> DeleteDTO(int id);

        Task<Machine> GetByName(string machineName);

        //Task<List<Machine>> GetByLocation(int locationId);

        Task<List<Machine>> GetByStatus(status status);

        Task<FilterMachine> GetAll(FitlerModel model);

        List<Machine> Search([FromQuery] FitlerModel model);

    }
    public class MachineServices : IMachine
    {
        private readonly LaundryContext _context;
        public MachineServices(LaundryContext context)
        {
            _context = context;
        }

        public async Task<MachineAddDTO> AddDTO(MachineAddDTO dto)
        {
            try
            {

            Machine machine = new Machine
            {
                MachineName = dto.MachineName,
                MachineType = (types)dto.MachineType,
                Branch = dto.Branch,
                Size = dto.Size,
                Status = (status)dto.Status

            };
            if (machine == null) return null;
            _context.Machines.Add(machine);
            var res = await _context.SaveChangesAsync();
            if (res < 1) return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: ", e.Message);
            }
            return new MachineAddDTO();

        }

        public async Task<MachineDeleteDTO> DeleteDTO(int id)
        {

            var dbMachine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == id);
            if (dbMachine == null)
                return null;
            _context.Machines.Remove(dbMachine);
            var res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new MachineDeleteDTO();
        }

        public async Task<Machine> GetById(int machineId)
        {
            if (machineId == null) return null;
            var dbMachine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == machineId);
            if (dbMachine == null)
                return null;
            return dbMachine;
        }


        public async Task<MachineUpdateDTO> UpdateDTO(MachineUpdateDTO dto)
        {
            var dbMachine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == dto.Id);
            if (dbMachine == null) return null;
            dbMachine.MachineName = dto.MachineName;
            dbMachine.MachineType = (types)dto.MachineType;
            dbMachine.Branch = dto.Branch;
            dbMachine.Size = dto.Size;
            dbMachine.Status = (status)dto.Status;

            int res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new MachineUpdateDTO();
        }
        public async Task<Machine> GetByName(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return null;

            var machine = await _context.Machines
               .Where(l => l.MachineName == machineName)
               .FirstOrDefaultAsync();
            return machine;
        }


        public async Task<List<Machine>> GetByStatus(status status)
        {
            var machine = await _context.Machines.Where(l => l.Status == status).ToListAsync();
            return machine;

        }
        //
        public List<Machine> Search(FitlerModel model)
        {
            if (!string.IsNullOrEmpty(model.search))
            {

                return _context.Machines.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).Select(e => new Machine
                {
                    MachineId = e.MachineId,
                    MachineName = e.MachineName,
                    MachineType = e.MachineType,
                    Branch = e.Branch,
                    Size = e.Size,
                    Status = e.Status
                }).ToListAsync().GetAwaiter().GetResult().Where(c => c.MachineName.Contains(model.search)).ToList();

            }
            return null;
        }


        public class FilterMachine : Paginate
        {
            public List<Machine> ListData { get; set; }

        }
        public async Task<FilterMachine> GetAll(FitlerModel model)
        {
            var total = await _context.Machines.CountAsync();
            var query = _context.Machines.Where(c => (!string.IsNullOrEmpty(model.search)) ? c.MachineName.Contains(model.search) : true);

            return new FilterMachine
            {

                TotalCount = total,
                ListData = await query.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize)
                .Select(e => new Machine
                {
                    MachineId = e.MachineId,
                    MachineName = e.MachineName,
                    MachineType = e.MachineType,
                    Branch = e.Branch,
                    Size = e.Size,
                    Status = e.Status
                }).ToListAsync(),
            };
        }

    }

}
