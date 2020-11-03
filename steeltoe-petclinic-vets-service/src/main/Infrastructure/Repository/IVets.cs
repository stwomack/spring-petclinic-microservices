using steeltoe_petclinic_vets_api.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_vets_api.Infrastructure.Repository {
  public interface IVets {
    IEnumerable<Vet> FindAll();
  }
}
