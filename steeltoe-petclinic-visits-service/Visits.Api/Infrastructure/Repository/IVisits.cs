using Petclinic.Visits.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Visits.Infrastructure.Repository
{
    public interface IVisits
    {
        Task<List<Visit>> FindByPetIdAsync(int petId, CancellationToken cancellationToken = default);

        Task<List<Visit>> FindByPetIdIn(IEnumerable<int> petIds);

        Task<Visit> SaveAsync(int petId, Visit visit, CancellationToken cancellationToken = default);
    }
}
