using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Laundry_Management.Models
{
    public partial class Machine
    {
        public Machine()
        {
            MachineModes = new HashSet<MachineMode>();
        }

        public int MachineId { get; set; }
        public string MachineName { get; set; } = null!;
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public types MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }
        public ulong? IsActive { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public status Status { get; set; }
        public virtual ICollection<MachineMode> MachineModes { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum status
        {
            [EnumMember(Value = "close")]
            close = 3 ,
            [EnumMember(Value = "loading")]
            loading =4,
            [EnumMember(Value = "done")]
            done = 5,
        }

        public enum types
        {
            [EnumMember(Value = "WM")]
            WashingMachine = 1,
            [EnumMember(Value = "D")]
            Dryer = 2,
        }
    }
}
