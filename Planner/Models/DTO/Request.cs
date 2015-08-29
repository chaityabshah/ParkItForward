using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Planner.Models.DTO
{
    public class Request
    {
        [JsonProperty(PropertyName = "VIN")]
        public string VIN { get; set; }
        public string type { get; set; }
        public string location { get; set; }
    }
}