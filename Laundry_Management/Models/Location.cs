using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class Location
    {
        public Location()
        {
            Machines = new HashSet<Machine>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string? Coordinates { get; set; }
        public ulong? IsActive { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }
    }
}
