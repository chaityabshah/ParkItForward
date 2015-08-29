using Newtonsoft.Json;

namespace Planner.Models.DTO
{
    public class Xedit
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public int PK { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}