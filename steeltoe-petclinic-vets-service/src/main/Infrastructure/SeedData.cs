using System.Threading;

namespace steeltoe_petclinic_vets_api.Infrastructure {
  internal static class SeedData
  {
  public static async void SeedAll(this VetsContext dbContext,
			bool ensureDelete = false,
			CancellationToken cancellationToken = default)
		{
			if(ensureDelete)
				dbContext.Database.EnsureDeleted();

			dbContext.Database.EnsureCreated();

      foreach (var vet in Fill.Vets)
        await dbContext.AddAsync(vet, cancellationToken);

      foreach (var vetSpecialty in Fill.VetSpecialties)
        await dbContext.AddAsync(vetSpecialty, cancellationToken);

			await dbContext.SaveChangesAsync(cancellationToken);
		}
  }
}
