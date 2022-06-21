using Newtonsoft.Json;
using System;

namespace KolPowt.Models.DTOs
{
    public class Prescription
    {
        [JsonProperty("id")]
        public int IdPrescription { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }
        [JsonProperty("idPatient")]
        public int IdPatient { get; set; }
        [JsonProperty("idDoctor")]
        public int IdDoctor { get; set; }
    }
}
