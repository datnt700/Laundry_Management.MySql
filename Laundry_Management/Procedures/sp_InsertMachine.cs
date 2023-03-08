using Laundry_Management.Models;

namespace Laundry_Management.Procedures
{
    public class sp_InsertMachine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; } = null!;
        public sbyte? MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }
        public ulong? IsActive { get; set; }
        public sbyte? Status { get; set; }
        public int? LocationId { get; set; }

        public virtual Location? Location { get; set; }
        public virtual ICollection<MachineMode> MachineModes { get; set; }
    }
}
