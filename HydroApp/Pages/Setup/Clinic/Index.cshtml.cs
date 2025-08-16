using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace HydroApp.Pages.Setup.Clinic;

public class IndexModel(IDbContextFactory<SpayWiseDbContext> dbFactory) : PageModel
{
	private readonly IDbContextFactory<SpayWiseDbContext> _dbFactory = dbFactory;

	[BindProperty]
    public SpayWise.Data.Clinic Clinic { get; set; } = new SpayWise.Data.Clinic();

    public void OnGet()
    {
        // TODO: Load clinic from database if needed
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