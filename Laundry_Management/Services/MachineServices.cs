using Laundry_Management.Models;

namespace Laundry_Management.Services
{
    public interface IMachine
    {
        Task<Machine> GetById(int machineId, int userId);
    }
    public class MachineServices : IMachine
    {
        public async Task<Machine> GetById(int machineId,int userId)
        {
            return new Machine();
        }
    }
}
