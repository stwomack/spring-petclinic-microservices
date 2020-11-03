
using System;
using System.Text.Json.Serialization;

namespace steeltoe_petclinic_customers_api.Domain {
  public partial class Pet
  {
    public Pet(string name, DateTime? birthDate, int petTypeId, int ownerId, int id = default){
      Id = id;
      Name = name ?? throw new ArgumentNullException(nameof(name));
      BirthDate = birthDate;
      PetTypeId = petTypeId;
      OwnerId = ownerId;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public int PetTypeId { get; private set; }
    public int OwnerId { get; private set; }

    [JsonIgnore]
    public virtual Owner Owner { get; set; }
    [JsonIgnore]
    public virtual PetType PetType { get; set; }
    
    public override string ToString()
    {
      return $@"id:{this.Id},
  name: {this.Name},
  birthDate: {this.BirthDate},
  type: {this.PetType},
  owner: {this.Owner}";
    }

    public void SetBirthDate(DateTime? birthDate) {
      BirthDate = birthDate;
    }
    public void SetName(string name){
      Name = name ?? throw new ArgumentNullException(nameof(name));
    }
  }
}
