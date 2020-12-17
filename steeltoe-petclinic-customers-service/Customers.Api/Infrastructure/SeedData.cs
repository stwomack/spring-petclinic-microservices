using System.Linq;
using System.Threading;

namespace Petclinic.Customers.Infrastructure
{
    internal static class SeedData
    {
        public static async void SeedAll(this CustomersContext dbContext, bool ensureDelete = false, CancellationToken cancellationToken = default)
        {
            if (ensureDelete)
            {
                dbContext.Database.EnsureDeleted();
            }

            dbContext.Database.EnsureCreated();

            if (!dbContext.PetTypes.Any())
            {
                foreach (var petType in Fill.PetTypes)
                {
                    await dbContext.AddAsync(petType, cancellationToken);
                }
            }

            if (!dbContext.Owners.Any())
            {
                foreach (var owner in Fill.Owners)
                {
                    await dbContext.AddAsync(owner, cancellationToken);
                }
            }

            if (!dbContext.Pets.Any())
            {
                foreach (var pet in Fill.Pets)
                {
                    await dbContext.AddAsync(pet, cancellationToken);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
