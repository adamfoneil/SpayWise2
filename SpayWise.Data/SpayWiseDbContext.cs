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
	public DbSet<VolumeClient> VolumeClients { get; set; }
	public DbSet<ClientPhone> ClientPhones { get; set; }

	public DbSet<Item> Items { get; set; }

	public DbSet<AppSpecies> AppSpecies { get; set; }
	public DbSet<Sex> Sexes { get; set; }

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

	public static string GetConnectionString(IConfiguration config, string[] args)
	{
		var connectionName = args.Length == 1 ? args[0] : Config.GetValue<string>("ConnectionName") ?? "DefaultConnection";
		return config.GetConnectionString(connectionName) ?? throw new Exception($"Connection string '{connectionName}' not found");
	}

	public SpayWiseDbContext CreateDbContext(string[] args)
	{
		var connectionString = GetConnectionString(Config, args);
		var optionsBuilder = new DbContextOptionsBuilder<SpayWiseDbContext>();
		optionsBuilder.UseNpgsql(connectionString);
		return new SpayWiseDbContext(optionsBuilder.Options);
	}
}