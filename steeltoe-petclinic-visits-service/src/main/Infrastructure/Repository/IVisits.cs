using steeltoe_petclinic_visits_api.Domain;
using steeltoe_petclinic_visits_api.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_visits_api.Infrastructure.Repository {
  public interface IVisits {
    Task<List<Visit>> FindByPetId(int petId, CancellationToken cancellationToken = default);
    IEnumerable<Visit> FindByPetIdIn(List<int> petIds);
    Task<Visit> Save(int petId, Visit visit, CancellationToken cancellationToken = default);
  }
}
