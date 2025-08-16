using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace HydroApp.Pages.Setup.Clinic;

public class IndexModel(
    IDbContextFactory<SpayWiseDbContext> dbFactory,
    CurrentClinicUserService currentUser) : PageModel
{
	private readonly IDbContextFactory<SpayWiseDbContext> _dbFactory = dbFactory;
	private readonly CurrentClinicUserService _currentUser = currentUser;

	[BindProperty]
    public SpayWise.Data.Clinic Clinic { get; set; } = new SpayWise.Data.Clinic();

    public async Task OnGetAsync()
    {
        using var db = _dbFactory.CreateDbContext();
        var (appUser, clinicUser) = await _currentUser.GetAsync();

		Clinic = (clinicUser is not null) ? await db.Clinics.FindAsync(clinicUser.ClinicId) ?? new SpayWise.Data.Clinic() : new();
        if (Clinic.OwnerUserId == 0) Clinic.OwnerUserId = appUser?.UserId ?? 0;
	}

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using var db = _dbFactory.CreateDbContext();
        db.Clinics.Update(Clinic);
        await db.SaveChangesAsync();

        return RedirectToPage();
    }
}