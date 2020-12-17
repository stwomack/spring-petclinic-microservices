using System.Linq;
using System.Threading;

namespace Petclinic.Vets.Infrastructure
{
    internal static class SeedData
    {
        public static async void SeedAll(this VetsContext dbContext, bool ensureDelete = false, CancellationToken cancellationToken = default)
        {
            if (ensureDelete)
            {
                dbContext.Database.EnsureDeleted();
            }

            dbContext.Database.EnsureCreated();

            if (!dbContext.Vets.Any())
            {
                foreach (var vet in Fill.Vets)
                {
                    await dbContext.AddAsync(vet, cancellationToken);
                }
            }

            if (!dbContext.Specialties.Any())
            {
                foreach (var specialty in Fill.Specialties)
                {
                    await dbContext.AddAsync(specialty, cancellationToken);
                }
            }

            if (!dbContext.VetSpecialties.Any())
            {
                foreach (var vetSpecialty in Fill.VetSpecialties)
                {
                    await dbContext.AddAsync(vetSpecialty, cancellationToken);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
