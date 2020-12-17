using Petclinic.Vets.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Petclinic.Vets.Infrastructure.Repository
{
    public class VetSpecialties : IVetSpecialties
    {
        private readonly VetsContext _dbContext;

        public VetSpecialties(VetsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<VetSpecialty> FindAllByVetId(int vetId)
        {
            return _dbContext.VetSpecialties.Where(q => q.VetId == vetId);
        }

        public IEnumerable<VetSpecialty> FindAllBySpecialtyId(int specialtyId)
        {
            return _dbContext.VetSpecialties.Where(q => q.SpecialtyId == specialtyId);
        }
    }
}
