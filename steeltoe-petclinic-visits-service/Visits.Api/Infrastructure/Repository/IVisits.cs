using Petclinic.Visits.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Visits.Infrastructure.Repository
{
    public interface IVisits
    {
        Task<List<Visit>> FindByPetIdAsync(int petId, CancellationToken cancellationToken = default);

        IEnumerable<Visit> FindByPetIdIn(List<int> petIds);

        Task<Visit> SaveAsync(int petId, Visit visit, CancellationToken cancellationToken = default);
    }
}
