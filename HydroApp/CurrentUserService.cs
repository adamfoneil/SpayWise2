using Microsoft.EntityFrameworkCore;
using SpayWise.Data;
using System.Security.Claims;

namespace HydroApp;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, SpayWiseDbContext dbContext)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly SpayWiseDbContext _dbContext = dbContext;

	public async Task<ClinicUser?> GetClinicUserAsync(ApplicationUser appUser) =>
		await _dbContext.ClinicUsers
			.FirstOrDefaultAsync(cu => cu.UserId == appUser.UserId && cu.ClinicId == appUser.CurrentClinicId);

	public async Task<(ApplicationUser?, ClinicUser?)> GetByNameAsync(string userName)
    {
		var appUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
		if (appUser == null) return (null, null);

		var clinicUser = await GetClinicUserAsync(appUser);

		if (clinicUser is not null)
		{
			clinicUser.ApplicationUser = appUser;
		}

		return (appUser, clinicUser);
	}

	public async Task<(ApplicationUser?, ClinicUser?)> GetAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true) return (null, null);

        var userName = user.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(userName)) return (null, null);

        return await GetByNameAsync(userName);
	}
}
