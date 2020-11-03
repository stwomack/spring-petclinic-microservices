using steeltoe_petclinic_vets_api.Domain;
using System.Collections.Generic;

namespace steeltoe_petclinic_vets_api.Infrastructure.Repository {
  public interface IVetSpecialties {
    IEnumerable<VetSpecialty> FindAllBySpecialtyId(int specialtyId);
    IEnumerable<VetSpecialty> FindAllByVetId(int vetId);
  }
}
