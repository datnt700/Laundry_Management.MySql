using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class Machine
    {
        public Machine()
        {
            MachineHistories = new HashSet<MachineHistory>();
            MachineModes = new HashSet<MachineMode>();
        }

        public int MachineId { get; set; }
        public string MachineName { get; set; } = null!;
        public sbyte? MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }
        public ulong? IsActive { get; set; }
        public sbyte? Status { get; set; }
        public int? LocationId { get; set; }

        public virtual Location? Location { get; set; }
        public virtual ICollection<MachineHistory> MachineHistories { get; set; }
        public virtual ICollection<MachineMode> MachineModes { get; set; }
    }
}
