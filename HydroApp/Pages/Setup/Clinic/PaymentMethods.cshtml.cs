using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace HydroApp.Pages.Setup.Clinic;

public class PaymentMethodsModel(
	CurrentUserService currentUser,
	IDbContextFactory<SpayWiseDbContext> dbFactory) : PageModel
{
	private readonly CurrentUserService _currentUser = currentUser;
	private readonly IDbContextFactory<SpayWiseDbContext> _dbFactory = dbFactory;

	private ClinicUser? _clinicUser;
	private ApplicationUser? _appUser;

	[BindProperty]
	public PaymentMethod EditingPaymentMethod { get; set; } = new();

	public PaymentMethod[] PaymentMethods { get; set; } = [];

	public async Task OnGetAsync()
	{
		(_appUser, _clinicUser) = await _currentUser.GetAsync();

		using var dbContext = _dbFactory.CreateDbContext();
		PaymentMethods = await dbContext.PaymentMethods.Where(row => row.ClinicId == _appUser!.CurrentClinicId).AsNoTracking().ToArrayAsync();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		(_appUser, _clinicUser) = await _currentUser.GetAsync();
		using var db = _dbFactory.CreateDbContext();		
		db.PaymentMethods.Update(EditingPaymentMethod);
		await db.SaveChangesAsync(_clinicUser!);
		return RedirectToPage();
	}
}
