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

	private ApplicationUser? _appUser;
	private ClinicUser? _clinicUser;

	[BindProperty]
    public int? SelectedClinicId { get; set; }

	[BindProperty]
    public SpayWise.Data.Clinic Clinic { get; set; } = new SpayWise.Data.Clinic();

	public SpayWise.Data.Clinic[] OwnedClinics { get; set; } = [];

    public async Task OnGetAsync()
    {
        using var db = _dbFactory.CreateDbContext();
        (_appUser, _clinicUser) = await _currentUser.GetAsync();

        OwnedClinics = await db.Clinics
            .Where(c => c.OwnerUserId == _appUser!.UserId)
            .OrderBy(c => c.Name)
            .ToArrayAsync();

        if (SelectedClinicId.HasValue)
        {
            Clinic = await db.Clinics.FindAsync(SelectedClinicId.Value) ?? new SpayWise.Data.Clinic();
        }
        else
        {
            Clinic = (_clinicUser is not null) ? await db.Clinics.FindAsync(_clinicUser.ClinicId) ?? new SpayWise.Data.Clinic() : new();
        }

        if (Clinic.OwnerUserId == 0) Clinic.OwnerUserId = _appUser?.UserId ?? 0;
	}

    public async Task<IActionResult> OnPostAsync()
    {
        using var db = _dbFactory.CreateDbContext();
        (_appUser, _clinicUser) = await _currentUser.GetAsync();

        OwnedClinics = await db.Clinics
            .Where(c => c.OwnerUserId == _appUser!.UserId)
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToArrayAsync();

        if (SelectedClinicId.HasValue)
        {
            Clinic = await db.Clinics.FindAsync(SelectedClinicId.Value) ?? new SpayWise.Data.Clinic();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        db.Clinics.Update(Clinic);
        await db.SaveChangesAsync(_clinicUser!);

        return RedirectToPage(new { SelectedClinicId });
    }
}