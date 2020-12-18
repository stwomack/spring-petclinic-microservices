using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petclinic.Visits.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Visits.Infrastructure.Repository
{
    internal class Visits : IVisits
    {
        private readonly ILogger<Visits> _logger;
        private readonly VisitsContext _dbContext;

        public Visits(ILogger<Visits> logger, VisitsContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<List<Visit>> FindByPetIdAsync(int petId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Visits.Where(q => q.PetId == petId).ToListAsync(cancellationToken);
        }

        public Task<List<Visit>> FindByPetIdIn(IEnumerable<int> petIds)
        {
            return _dbContext.Visits.Where(q => petIds.Any(a => a == q.PetId)).ToListAsync();
        }

        public async Task<Visit> SaveAsync(int petId, Visit visit, CancellationToken cancellationToken = default)
        {
            visit.SetPetId(petId);

            _dbContext.Visits.Add(visit);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return visit;
        }
    }
}
