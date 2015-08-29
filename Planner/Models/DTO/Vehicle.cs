using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Planner.Models.DTO
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vehicles
    {
        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }

        [JsonProperty(PropertyName = "vehicle")]
        public IEnumerable<Vehicle> Vehicle { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Vehicle
    {
        [JsonProperty(PropertyName = "vin")]
        public int Vin { get; set; }

        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "unitType")]
        public string UnitType { get; set; }

        [JsonProperty(PropertyName = "onstartStatus")]
        public string OnstarStatus { get; set; }

        [JsonProperty(PropertyName = "primaryDriverID")]
        public string PrimaryDriverId { get; set; }

        [JsonProperty(PropertyName = "primaryDriverUrl")]
        public string PrimaryDriverUrl { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "isInPreActivation")]
        public string IsInPreActivation { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MapVehicle
    {
        [JsonProperty(PropertyName = "vin")]
        public int Vin { get; set; }

        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "unitType")]
        public string UnitType { get; set; }

        [JsonProperty(PropertyName = "onstartStatus")]
        public string OnstarStatus { get; set; }

        [JsonProperty(PropertyName = "primaryDriverID")]
        public string PrimaryDriverId { get; set; }

        [JsonProperty(PropertyName = "primaryDriverUrl")]
        public string PrimaryDriverUrl { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "isInPreActivation")]
        public string IsInPreActivation { get; set; }
    }
}