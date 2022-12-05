using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class ServiceMode
    {
        public ServiceMode()
        {
            MachineModes = new HashSet<MachineMode>();
        }

        public int ModeId { get; set; }
        public string ModeName { get; set; } = null!;
        public ulong? IsActive { get; set; }
        public double? PricePerMinute { get; set; }
        public double? PricePerSize { get; set; }

        public virtual ICollection<MachineMode> MachineModes { get; set; }
    }
}
