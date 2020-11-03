using steeltoe_petclinic_customers_api.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_customers_api.Repository {
  public interface IPets {
    Task<List<Pet>> FindAll(CancellationToken cancellationToken = default);
    Task<List<Pet>> FindAll(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Pet> FindById(int id, CancellationToken cancellationToken = default);
    Task<PetType> FindPetTypeById(int id, CancellationToken cancellationToken = default);
    Task<List<PetType>> FindPetTypes(CancellationToken cancellationToken = default);
    Task Save(Pet pet, CancellationToken cancellationToken = default);
    Task Update(Pet pet, CancellationToken cancellationToken = default);
  }
}
