using System.Threading;

namespace steeltoe_petclinic_visits_api.Infrastructure
{
  public static class SeedData
  {
  public static async void SeedAll(this VisitsContext dbContext,
			bool ensureDelete = false,
			CancellationToken cancellationToken = default)
		{
			if(ensureDelete)
				dbContext.Database.EnsureDeleted();

			dbContext.Database.EnsureCreated();

      foreach (var visit in Fill.Visits)
        await dbContext.AddAsync(visit, cancellationToken);

			await dbContext.SaveChangesAsync(cancellationToken);
		}
  }
}
