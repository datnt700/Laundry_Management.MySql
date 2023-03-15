using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class Location
    {
        

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public string? Coordinates { get; set; }
        public ulong? IsActive { get; set; }

    }
}
