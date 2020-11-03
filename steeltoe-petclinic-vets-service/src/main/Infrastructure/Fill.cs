using System;
using System.Globalization;
using steeltoe_petclinic_vets_api.Domain;

namespace steeltoe_petclinic_vets_api.Infrastructure {
  public static class Fill
  {
    public static Vet[] Vets => new[]{
      new Vet(
        "James",
        "Carter",
        id:1
      ),
      new Vet(
        "Helen",
        "Leary",
        id:2
      ),
      new Vet(
        "Linda",
        "Douglas",
        id:3
      ),
      new Vet(
        "Rafael",
        "Ortega",
        id:4
      ),
      new Vet(
        "Henry",
        "Stevens",
        id:5
      ),
      new Vet(
        "Sharon",
        "Jenkins",
        id:6
      )
    };
    public static VetSpecialty[] VetSpecialties => new[]{
      new VetSpecialty(
        2,
        Specialty.Radiology.Id
      ),
      new VetSpecialty(
        3,
        Specialty.Surgery.Id
      ),
      new VetSpecialty(
        3,
        Specialty.Dentistry.Id
      ),
     new VetSpecialty(
        4,
        Specialty.Surgery.Id
      ),
      new VetSpecialty(
        5,
        Specialty.Radiology.Id
      )
    };
  }
}
