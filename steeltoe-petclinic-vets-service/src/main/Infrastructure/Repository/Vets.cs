using Microsoft.EntityFrameworkCore;
using steeltoe_petclinic_vets_api.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_vets_api.Infrastructure.Repository {
  public class Vets : IVets {
    private readonly VetsContext _dbContext;

    public Vets(VetsContext dbContext) {
      _dbContext = dbContext;
    }

    public IEnumerable<Vet> FindAll() {
      return _dbContext.Vets;
    }
  }
}
