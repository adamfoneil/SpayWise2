using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

namespace Testing
{
    [TestClass]
    public class ClinicIntegrationTests
    {
        private static SpayWiseDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SpayWiseDbContext>()
                .UseInMemoryDatabase(databaseName: "ClinicIntegrationTestDb")
                .Options;
            var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<SpayWiseDbContext>();
            return new SpayWiseDbContext(options, logger);
        }

		private ClinicUser CreateUserWithManageClinic(ApplicationUser appUser) => new()
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
            var db = CreateDbContext();
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
    }
}
