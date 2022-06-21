using Newtonsoft.Json;
using System.Collections.Generic;

namespace KolPowt.Models.DTOs
{
    public class GetMedicament
    {
        [JsonProperty("id")]
        public int IdMedicament { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("prescriptions")]
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
