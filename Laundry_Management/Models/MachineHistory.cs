using System;
using System.Collections.Generic;

namespace Laundry_Management.Models
{
    public partial class MachineHistory
    {
        public int HistoryId { get; set; }
        public int? UserId { get; set; }
        public TimeOnly? TimeUse { get; set; }
        public sbyte? Status { get; set; }
        public long? Money { get; set; }
        public int? LocationId { get; set; }
        public int? MachineId { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Machine? Machine { get; set; }
        public virtual User? User { get; set; }
    }
}
