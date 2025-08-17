using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpayWise.Data;

namespace Services;

public class ApplicationSignInManager(
	IDbContextFactory<SpayWiseDbContext> dbFactory,	
	UserManager<ApplicationUser> userManager, 
	IHttpContextAccessor contextAccessor, 
	IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
	IOptions<IdentityOptions> optionsAccessor, 
	ILogger<SignInManager<ApplicationUser>> logger, 
	IAuthenticationSchemeProvider schemes, 
	IUserConfirmation<ApplicationUser> confirmation) : SignInManager<ApplicationUser>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
{
	private readonly IDbContextFactory<SpayWiseDbContext> _dbFactory = dbFactory;	

	public override async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
	{
		var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

		if (result.Succeeded)			
		{			
			using var db = _dbFactory.CreateDbContext();
			await db.Users.Where(row => row.UserId == user.UserId).ExecuteUpdateAsync(u => u.SetProperty(row => row.LastLoginDate, DateTime.UtcNow));
		}

		return result;
	}

	public override async Task<bool> CanSignInAsync(ApplicationUser user)
	{
		var result = await base.CanSignInAsync(user);

		if (result)
		{	
			using var db = _dbFactory.CreateDbContext();
			var clinicUser = await db.ClinicUsers.Where(row => row.UserId == user.UserId && row.ClinicId == user.CurrentClinicId).SingleOrDefaultAsync();
			if (clinicUser == null) return true;
			return clinicUser.IsEnabled;
		}

		return result;
	}
}
