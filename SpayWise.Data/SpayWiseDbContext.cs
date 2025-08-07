using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpayWise.Data;

public class SpayWiseDbContext(DbContextOptions<SpayWiseDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<Clinic> Clinics { get; set; }
	public DbSet<ClinicUser> ClinicUsers { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<ClientPhone> ClientPhones { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(SpayWiseDbContext).Assembly);
	}
}

public class AppDbFactory : IDesignTimeDbContextFactory<SpayWiseDbContext>
{
	private static IConfiguration Config => new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: false)
		.AddUserSecrets("24057544-6aba-4d06-8cd3-66192e2e69b8")
		.Build();

	public SpayWiseDbContext CreateDbContext(string[] args)
	{
		var connectionName = args.Length == 1 ? args[0] : Config.GetValue<string>("ConnectionName") ?? "DefaultConnection";
		var connectionString = Config.GetConnectionString(connectionName) ?? throw new Exception($"Connection string '{connectionName}' not found");
		var optionsBuilder = new DbContextOptionsBuilder<SpayWiseDbContext>();
		optionsBuilder.UseNpgsql(connectionString);
		return new SpayWiseDbContext(optionsBuilder.Options);
	}
}