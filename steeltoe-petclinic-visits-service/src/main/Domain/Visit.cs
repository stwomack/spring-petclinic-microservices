using System;

namespace steeltoe_petclinic_visits_api.Domain {
  public partial class Visit {
    public Visit(int petId, DateTime? date, string description, int id = default) {
      Id = id;
      PetId = petId;
      Date = date;
      Description = description;
    }

    public int Id { get; private set; }
    public int PetId { get; private set; }
    public DateTime? Date { get; private set; }
    public string Description { get; private set; }

    public void SetPetId(int petId){
      PetId = petId;
    }
  }
}
