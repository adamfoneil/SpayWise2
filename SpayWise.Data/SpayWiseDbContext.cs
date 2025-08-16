using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpayWise.Data.Conventions;
using SpayWise.Data.Interfaces;

namespace SpayWise.Data;

public class SpayWiseDbContext(
	DbContextOptions<SpayWiseDbContext> options,
	ILogger<SpayWiseDbContext> logger) : IdentityDbContext<ApplicationUser>(options)
{
	private readonly ILogger<SpayWiseDbContext> _logger = logger;

	// system tables
	public DbSet<AppSpecies> AppSpecies { get; set; }
	public DbSet<Sex> Sexes { get; set; }

	// core tables
	public DbSet<Clinic> Clinics { get; set; }	
	public DbSet<Location> Locations { get; set; }
	public DbSet<ClinicUser> ClinicUsers { get; set; }
	public DbSet<Client> Clients { get; set; }
	public DbSet<VolumeClient> VolumeClients { get; set; }
	public DbSet<ClientPhone> ClientPhones { get; set; }	
	public DbSet<Species> Species { get; set; }
	public DbSet<Item> Items { get; set; }
	public DbSet<AppointmentType> AppointmentTypes { get; set; }

	// capacity
	public DbSet<DailyCountLimit> DailyCountLimits { get; set; }
	public DbSet<DailyPointsLimit> DailyPointLimits { get; set; }
	public DbSet<CountLimitOverride> OverrideCounts { get; set; }
	public DbSet<PointsLimitOverride> OverridePoints { get; set; }
	// todo: kennel space

	// satellite tables
	public DbSet<Veterinarian> Veterinarians { get; set; }
	public DbSet<DeclineReason> DeclineReasons { get; set; }
	public DbSet<PaymentMethod> PaymentMethods { get; set; }

	private static Dictionary<Type, Permissions> RequiredPermissions => new()
	{
		[typeof(Clinic)] = Permissions.ManageClinic,
		[typeof(Species)] = Permissions.ManageClinic,
		[typeof(VolumeClient)] = Permissions.ManageVolumeClients,
		[typeof(ClinicUser)] = Permissions.ManageUsers,
		[typeof(Item)] = Permissions.ManageItems,
		[typeof(DailyCountLimit)] = Permissions.ManageCapacity,
		[typeof(DailyPointsLimit)] = Permissions.ManageCapacity,
		[typeof(CountLimitOverride)] = Permissions.ManageCapacity,
		[typeof(PointsLimitOverride)] = Permissions.ManageCapacity,
		[typeof(Veterinarian)] = Permissions.ManageClinic,
		[typeof(DeclineReason)] = Permissions.ManageClinic,
		[typeof(PaymentMethod)] = Permissions.ManageClinic,
	};

	public async Task<int> SaveChangesAsync(ClinicUser user)
	{
		if (user is null) throw new ArgumentNullException(nameof(user));
		if (!user.IsEnabled) throw new InvalidOperationException("User is disabled");

		ThrowIfNoPermission(user);

		SetClinicId(user);

		AuditEntities(user.ApplicationUser);

		return await base.SaveChangesAsync();
	}

	private void SetClinicId(ClinicUser user)
	{
		foreach (var entry in ChangeTracker.Entries())
		{
			if (entry.Entity is IClinicEntity clinicEntity)
			{
				if (entry.State == EntityState.Added)
				{					
					clinicEntity.ClinicId = user.ClinicId;
				}
				else if (entry.State == EntityState.Modified)
				{
					if (clinicEntity.ClinicId != user.ClinicId)
					{
						throw new InvalidOperationException("Cannot change ClinicId of an entity.");
					}
				}
			}
		}
	}

	private void ThrowIfNoPermission(ClinicUser user)
	{
		foreach (var entry in ChangeTracker.Entries())
		{
			var entityType = entry.Entity.GetType();
			if (RequiredPermissions.TryGetValue(entityType, out var requiredPermission))
			{
				if (!user.Permissions.HasFlag(requiredPermission))
				{
					throw new InvalidOperationException($"User does not have required permission '{requiredPermission}' for entity type '{entityType.Name}'.");
				}
			}
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(SpayWiseDbContext).Assembly);
	}

	private void AuditEntities(ApplicationUser? user)
	{
		foreach (var entity in ChangeTracker.Entries<BaseTable>())
		{
			switch (entity.State)
			{
				case EntityState.Added:
					entity.Entity.CreatedBy = user?.UserName ?? "system";
					entity.Entity.CreatedAt = LocalDateTime(user?.TimeZoneId);
					break;
				case EntityState.Modified:
					entity.Entity.UpdatedBy = user?.UserName ?? "system";
					entity.Entity.UpdatedAt = LocalDateTime(user?.TimeZoneId);
					break;
			}
		}
	}

	private static DateTime LocalDateTime(string? timeZoneId)
	{
		var now = DateTime.Now;
		if (string.IsNullOrWhiteSpace(timeZoneId)) return now;

		try
		{
			var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			var localTime = TimeZoneInfo.ConvertTimeFromUtc(now, timeZone);
			return localTime;
		}
		catch
		{
			return DateTime.Now;
		}
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
		var logger = new LoggerFactory().CreateLogger<SpayWiseDbContext>();
		return new SpayWiseDbContext(optionsBuilder.Options, logger);
	}
}