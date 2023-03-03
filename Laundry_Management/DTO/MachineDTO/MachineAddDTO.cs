using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Laundry_Management.DTO.MachineDTO
{
    public class MachineAddDTO
    {
       
        public string MachineName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public types MachineType { get; set; }
        public string? Branch { get; set; }
        public string? Size { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public status Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum status
        {
            [EnumMember(Value = "close")]
            close = 3,
            [EnumMember(Value = "loading")]
            loading = 4,
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
