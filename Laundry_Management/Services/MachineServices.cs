using Laundry_Management.Common;
using Laundry_Management.Data;
using Laundry_Management.DTO.MachineDTO;
using Laundry_Management.DTO.UserDTO;
using Laundry_Management.Models;
using Microsoft.EntityFrameworkCore;
using static Laundry_Management.Models.Machine;

namespace Laundry_Management.Services
{
    public interface IMachine
    {
        Task<Machine> GetById(int machineId, int userId);
        Task<MachineUpdateDTO> UpdateDTO(MachineUpdateDTO dto);

        Task<MachineAddDTO> AddDTO(MachineAddDTO dto);

        Task<MachineDeleteDTO> DeleteDTO(MachineDeleteDTO dto);

        Task<Machine> GetByName(string machineName);

        Task<List<Machine>> GetByLocation(int locationId);

        Task<List<Machine>> GetByStatus(status status);
    }
    public class MachineServices : IMachine
    {
        private readonly LaundryContext _context;
        public MachineServices (LaundryContext context)
        {
            _context = context;
        }

        public async Task<MachineAddDTO> AddDTO(MachineAddDTO dto)
        {
            Machine machine = new Machine
            {
                MachineName = dto.MachineName,
                MachineType = dto.MachineType,
                Branch = dto.Branch,
                Size = dto.Size
            };
            if (machine == null) return null;
            _context.Machines.Add(machine);
            var res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new MachineAddDTO();

        }

        public  async Task<MachineDeleteDTO> DeleteDTO(MachineDeleteDTO dto)
        {
            var dbMachine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == dto.Id);
            if (dbMachine == null)
                return null;
            _context.Machines.Remove(dbMachine);
            var res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new MachineDeleteDTO();
        }

        public async Task<Machine> GetById(int machineId,int userId)
        {
            return new Machine();
        }


        public async Task<MachineUpdateDTO> UpdateDTO(MachineUpdateDTO dto)
        {
            var dbMachine = await _context.Machines.FirstOrDefaultAsync(m => m.MachineId == dto.Id);
            if (dbMachine == null) return null;
            dbMachine.MachineName = dto.MachineName;
            dbMachine.MachineType= dto.MachineType;
            dbMachine.Branch = dto.Branch;
            dbMachine.Size= dto.Size;

            int res = await _context.SaveChangesAsync();
            if (res < 1) return null;
            return new MachineUpdateDTO();
        }
        public async Task<Machine> GetByName(string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName)) return null;

            var machine = await _context.Machines
               //.Include(l => l.Location)
               .Where(l => l.MachineName == machineName)
               .FirstOrDefaultAsync();
            return machine;
        }

        public async Task<List<Machine>> GetByLocation(int locationId)
        {
            if (locationId == null) return null;

            var machine = await _context.Machines
               .Where(l => l.LocationId == locationId)
               .ToListAsync();
            return machine;
        }

        

        public async Task<List<Machine>> GetByStatus(status status)
        {
            var machine = await _context.Machines.Where(l => l.Status == status).ToListAsync();
            return machine;

        }
    }

   
}
