using Petclinic.Vets.Domain;
using System.Collections.Generic;

namespace Petclinic.Vets.Infrastructure.Repository
{
    public class Vets : IVets
    {
        private readonly VetsContext _dbContext;

        public Vets(VetsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Vet> FindAll()
        {
            return _dbContext.Vets;
        }
    }
}
