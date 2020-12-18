using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petclinic.Customers.Domain;
using Petclinic.Customers.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Customers.Repository
{
    internal class Pets : IPets
    {
        private readonly ILogger<Pets> _logger;
        private readonly CustomersContext _dbContext;

        public Pets(ILogger<Pets> logger, CustomersContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<List<PetType>> FindPetTypes(CancellationToken cancellationToken = default)
        {
            return _dbContext.PetTypes.OrderByDescending(q => q.Name).ToListAsync(cancellationToken);
        }

        public Task<PetType> FindPetTypeById(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.PetTypes.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        }

        public Task<Pet> FindById(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Pets.Include(b => b.Owner).Include(b => b.PetType).FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
        }

        public Task<List<Pet>> FindAll(CancellationToken cancellationToken = default)
        {
            return _dbContext.Pets.Include(b => b.Owner).Include(b => b.PetType).ToListAsync(cancellationToken);
        }

        public Task<List<Pet>> FindAll(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return _dbContext.Pets.Include(b => b.Owner).Include(b => b.PetType).Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task Save(Pet pet, CancellationToken cancellationToken = default)
        {
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Pet pet, CancellationToken cancellationToken = default)
        {
            _dbContext.Pets.Update(pet);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        //public Task Delete(Pet pet, CancellationToken cancellationToken = default)
        //{
        //  _dbContext.Pets.Remove(pet);
        //  return _dbContext.SaveChangesAsync(cancellationToken);
        //}
    }
}
