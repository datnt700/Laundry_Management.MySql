namespace Laundry_Management.Procedures
{
    public class sp_GetMachineById
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; } = null!;
        public sbyte MachineType { get; set; }
        public string Branch { get; set; } = null!;
        public string Size { get; set; } = null!;
        public ulong IsActive { get; set; }
        public sbyte Status { get; set; }
    }
}
