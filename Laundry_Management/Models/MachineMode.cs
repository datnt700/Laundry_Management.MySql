using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class MachineMode
    {
        public int MachineModeId { get; set; }
        public int? ServiceModeId { get; set; }
        public int? MachineId { get; set; }

        public virtual Machine? Machine { get; set; }
        public virtual ServiceMode? ServiceMode { get; set; }
    }
}
