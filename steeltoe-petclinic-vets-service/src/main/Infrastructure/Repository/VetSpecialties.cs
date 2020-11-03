using Microsoft.EntityFrameworkCore;
using steeltoe_petclinic_vets_api.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_vets_api.Infrastructure.Repository {
  public class VetSpecialties : IVetSpecialties {
    private readonly VetsContext _dbContext;

    public VetSpecialties(VetsContext dbContext) {
      _dbContext = dbContext;
    }

    public IEnumerable<VetSpecialty> FindAllByVetId(int vetId) {
      return _dbContext.VetSpecialties.Where(q => q.VetId == vetId);
    }
    public IEnumerable<VetSpecialty> FindAllBySpecialtyId(int specialtyId) {
      return _dbContext.VetSpecialties.Where(q => q.SpecialtyId == specialtyId);
    }
  }
}
