using Microsoft.EntityFrameworkCore;
using SpayWise.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Testing
{
	[TestClass]
	public class ClinicIntegrationTests
	{
		private static SpayWiseDbContext InMemoryDbContext()
		{			
			var options = new DbContextOptionsBuilder<SpayWiseDbContext>()
				.UseInMemoryDatabase(databaseName: "ClinicIntegrationTestDb")
				.Options;
			var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<SpayWiseDbContext>();
			return new SpayWiseDbContext(options, logger);			
		}

		private static ClinicUser CreateUserWithManageClinic(ApplicationUser appUser) => new()
		{
			UserId = appUser.UserId,
			IsEnabled = true,
			Permissions = Permissions.ManageClinic,
			ApplicationUser = appUser
		};

		[TestMethod]
		public async Task UserWithManageClinicPermission_CanInsertAndUpdateClinic()
		{
			// Arrange
			var db = InMemoryDbContext();
			var appUser = new ApplicationUser { UserName = "testuser", UserId = 1 };
			var clinicUser = CreateUserWithManageClinic(appUser);

			var clinic = new Clinic
			{
				Name = "Test Clinic",
				Address1 = "123 Main St",
				City = "Testville",
				State = "TS",
				ZipCode = "12345",
				PrimaryPhone = "555-1234",
				Email = "test@clinic.com",
				OwnerUserId = appUser.UserId
			};

			db.Clinics.Add(clinic);
			var insertResult = await db.SaveChangesAsync(clinicUser);
			Assert.AreEqual(1, insertResult);

			// Act - Update
			clinic.Name = "Updated Clinic Name";
			db.Clinics.Update(clinic);
			var updateResult = await db.SaveChangesAsync(clinicUser);
			Assert.AreEqual(1, updateResult);

			// Assert
			var updatedClinic = db.Clinics.FirstOrDefault(c => c.Id == clinic.Id);
			Assert.IsNotNull(updatedClinic);
			Assert.AreEqual("Updated Clinic Name", updatedClinic.Name);
		}

		[TestMethod]
		public async Task InsertClinic_PostgresIntegration_IdempotentAndAssertFound()
		{
			// Arrange: Load connection string from HydroApp/appsettings.json
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
				.AddUserSecrets("24057544-6aba-4d06-8cd3-66192e2e69b8")
				.Build();
			var connectionString = config.GetConnectionString(config["ConnectionName"] ?? "Local");

			var options = new DbContextOptionsBuilder<SpayWiseDbContext>()
				.UseNpgsql(connectionString)
				.Options;
			var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<SpayWiseDbContext>();
			var db = new SpayWiseDbContext(options, logger);

			// Set a known time zone for the test user
			var testTimeZoneId = "Eastern Standard Time";
			var appUser = new ApplicationUser { UserName = "testuser", UserId = 1, TimeZoneId = testTimeZoneId };
			var clinicUser = CreateUserWithManageClinic(appUser);

			var clinic = new Clinic
			{
				Name = "Postgres Clinic",
				Address1 = "456 Main St",
				City = "PGVille",
				State = "PG",
				ZipCode = "54321",
				PrimaryPhone = "555-4321",
				Email = "pg@clinic.com",
				OwnerUserId = appUser.UserId
			};

			// Clean up any previous test row
			var existing = db.Clinics.FirstOrDefault(c => c.Name == clinic.Name && c.Email == clinic.Email);
			if (existing != null)
			{
				db.Clinics.Remove(existing);
				await db.SaveChangesAsync(clinicUser);
			}

			// Capture expected UTC time
			var utcNow = DateTime.UtcNow;
			var tz = TimeZoneInfo.FindSystemTimeZoneById(testTimeZoneId);
			var localTime = TimeZoneInfo.ConvertTime(utcNow, tz);			

			db.Clinics.Add(clinic);
			await db.SaveChangesAsync(clinicUser);

			// Assert: Clinic row is found
			var found = db.Clinics.FirstOrDefault(c => c.Name == clinic.Name && c.Email == clinic.Email);
			Assert.IsNotNull(found);
			Assert.AreEqual("Postgres Clinic", found.Name);

			// Assert: CreatedAt is close to expected UTC time
			var delta = (found.CreatedAt - localTime).TotalSeconds;
			Assert.IsTrue(Math.Abs(delta) < 10, $"CreatedAt {found.CreatedAt} not within 10 seconds of expected {localTime}");
		}
	}
}
