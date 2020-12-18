using System;
using System.Text.Json.Serialization;

namespace Petclinic.Visits.DTOs
{
    public class VisitRequest
    {
        public VisitRequest() { }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? VisitDate { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"visit request on {VisitDate} with description {Description}";
        }
    }
}
