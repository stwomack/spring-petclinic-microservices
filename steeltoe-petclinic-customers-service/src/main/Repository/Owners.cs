using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using steeltoe_petclinic_customers_api.Domain;
using steeltoe_petclinic_customers_api.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace steeltoe_petclinic_customers_api.Repository
{
  internal class Owners : IOwners {
    private readonly ILogger<Owners> _logger;
    private readonly CustomersContext _dbContext;

    public Owners(ILogger<Owners> logger, CustomersContext dbContext) {
      _logger = logger;
      _dbContext = dbContext;
    }

    public Task<Owner> FindById(int id, CancellationToken cancellationToken = default) {
      return _dbContext.Owners.Include(b => b.Pets).ThenInclude(p => p.PetType).FirstAsync(q => q.Id == id, cancellationToken);
    }

    public Task<List<Owner>> FindAll(CancellationToken cancellationToken = default) {
      return _dbContext.Owners.Include(b => b.Pets).ThenInclude(p => p.PetType).ToListAsync(cancellationToken);
    }

    public Task<List<Owner>> FindAll(int page, int pageSize, CancellationToken cancellationToken = default) {
      return _dbContext.Owners.Include(b => b.Pets).ThenInclude(p => p.PetType).Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<Owner> Save(Owner owner, CancellationToken cancellationToken = default) {
      _dbContext.Owners.Add(owner);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return owner;
    }
    public async Task<Owner> Update(Owner owner, Owner newOwnerVals, CancellationToken cancellationToken = default) {
      owner.SetFirstName(newOwnerVals.FirstName);
      owner.SetLastName(newOwnerVals.LastName);
      owner.SetCity(newOwnerVals.City);
      owner.SetAddress(newOwnerVals.Address);
      owner.SetTelephone(newOwnerVals.Telephone);

      _dbContext.Owners.Update(owner);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return owner;
    }
    public Task Delete(Owner Owner, CancellationToken cancellationToken = default) {
      _dbContext.Owners.Remove(Owner);
      return _dbContext.SaveChangesAsync(cancellationToken);
    }
  }
}
