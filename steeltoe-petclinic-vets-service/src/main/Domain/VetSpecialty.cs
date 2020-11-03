using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace steeltoe_petclinic_vets_api.Domain
{
    public class VetSpecialty
    {
     
    public VetSpecialty(int vetId, int specialtyId) {
      VetId = vetId;
      SpecialtyId = specialtyId;
    }

    public int VetId { get; private set; }
    public Vet Vet { get; set; }

    public int SpecialtyId { get; private set; }
    public Specialty Specialty => Specialty.GetAll().First(q => q.Id == this.SpecialtyId);
    }
}
