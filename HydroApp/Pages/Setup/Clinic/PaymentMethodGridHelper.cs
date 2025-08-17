using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace HydroApp.Pages.Setup.Clinic;

public class PaymentMethodGridHelper(
	CurrentUserService currentUser,
	IDbContextFactory<SpayWiseDbContext> dbFactory) : GridHelper<PaymentMethod>(currentUser, dbFactory)
{
	protected override IQueryable<PaymentMethod> GetQuery(SpayWiseDbContext dbContext)
	{

		dbContext.PaymentMethods.Where(row => row.ClinicId == CurrentUser.ClinicUser!.ClinicId);
	}
		
}
