using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace HydroApp;

public abstract class GridHelper<TEntity>(
	CurrentUserService currentUser,
	IDbContextFactory<SpayWiseDbContext> dbFactory) where TEntity : class
{
	protected readonly CurrentUserService CurrentUser = currentUser;
	protected readonly IDbContextFactory<SpayWiseDbContext> DbFactory = dbFactory;

	protected abstract IQueryable<TEntity> GetQuery(SpayWiseDbContext dbContext);

	public async Task<TEntity[]> QueryAsync()
	{
		var (appUser, clinicUser) = await CurrentUser.GetAsync();
		
		using var dbContext = DbFactory.CreateDbContext();
		var query = GetQuery(dbContext).Where(row => EF.Property<int>(row, "ClinicId") == clinicUser!.ClinicId);
		return await query.AsNoTracking().ToArrayAsync();
	}
}
