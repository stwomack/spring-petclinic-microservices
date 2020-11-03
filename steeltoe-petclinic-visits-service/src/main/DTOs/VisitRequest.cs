using System;
using System.Text.Json.Serialization;

namespace steeltoe_petclinic_visits_api.DTOs {
  public class VisitRequest {
    public VisitRequest() { }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? VisitDate { get; set; }
    public string Description { get; set; }
  }
}
