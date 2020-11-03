using steeltoe_petclinic_customers_api.Domain;
using System;
using System.Text.Json.Serialization;

namespace steeltoe_petclinic_customers_api.DTOs {
  public class PetRequest
  {
    public PetRequest() { }
    public PetRequest(int id, DateTime? birthDate, string name, string petTypeId){
      this.Id = id;
      this.BirthDate = birthDate;
      this.Name = name;
      this.TypeId = petTypeId;
    }
    public int Id { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? BirthDate { get; set; }

    public string Name { get; set; }

    public string TypeId { get; set; }
  }
}
