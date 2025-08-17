using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpayWise.Data;

namespace Services;

public class ApplicationUserManager(
	IDbContextFactory<SpayWiseDbContext> dbFactory,
	IUserStore<ApplicationUser> store, 
	IOptions<IdentityOptions> optionsAccessor, 
	IPasswordHasher<ApplicationUser> passwordHasher, 
	IEnumerable<IUserValidator<ApplicationUser>> userValidators, 
	IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, 
	ILookupNormalizer keyNormalizer, 
	IdentityErrorDescriber errors, 
	IServiceProvider services, 
	ILogger<UserManager<ApplicationUser>> logger) : UserManager<ApplicationUser>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
{
	private readonly IDbContextFactory<SpayWiseDbContext> _dbFactory = dbFactory;

	public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
	{
		var result = await base.CreateAsync(user, password);

		if (result.Succeeded)
		{
			using var db = _dbFactory.CreateDbContext();
			var appUser = await db.Users.SingleOrDefaultAsync(u => u.UserName == user.UserName) ?? throw new Exception("User not found after registering");
			var userId = appUser.UserId;

			// Create default Clinic for the new user
			var clinic = new Clinic
			{
				Name = $"{appUser.UserName}'s Clinic",
				Address1 = "",
				City = "",
				State = "",
				ZipCode = "",
				PrimaryPhone = "",
				Email = appUser.Email ?? $"{appUser.UserName}@example.com",
				OwnerUserId = userId,
				IsActive = true,
				TimeZoneId = appUser.TimeZoneId
			};
			// Create ClinicUser for the new user
			var clinicUser = new ClinicUser
			{
				Clinic = clinic,
				UserId = userId,
				IsEnabled = true,
				Permissions = Permissions.All,
				ApplicationUser = appUser
			};
			// Add entities
			await db.Clinics.AddAsync(clinic);
			await db.ClinicUsers.AddAsync(clinicUser);
			await db.SaveChangesAsync(clinicUser);

			// Set user's CurrentClinicId
			appUser.CurrentClinicId = clinic.Id;
			db.Users.Update(appUser);
			await db.SaveChangesAsync(clinicUser);
		}

		return result;
	}

}
