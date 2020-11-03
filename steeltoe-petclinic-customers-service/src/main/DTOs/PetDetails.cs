using steeltoe_petclinic_customers_api.Domain;
using steeltoe_petclinic_customers_api.DTOs;
using System;
using System.Text.Json.Serialization;

namespace steeltoe_petclinic_customers_api.DTOs
{
  
  public class PetDetails
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Owner { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? BirthDate{ get; set; }

    public PetType Type { get; set; }

    public PetDetails() { }
    public PetDetails(int id, string name, string owner, DateTime? birthDate, PetType petType)
    {
      this.Id = id;
      this.Name = name;
      this.Owner = owner;
      this.BirthDate = birthDate;
      this.Type = petType;
    }
  }
}
