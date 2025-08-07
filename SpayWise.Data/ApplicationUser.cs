using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpayWise.Data;

public class ApplicationUser : IdentityUser
{
	public int UserId { get; set; }
	public string? TimeZoneId { get; set; }
	public int? ClinicId { get; set; }
}

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.HasIndex(u => u.UserId).IsUnique();
		builder.Property(u => u.UserId).ValueGeneratedOnAdd();		
		builder.Property(u => u.TimeZoneId).HasMaxLength(50);
	}
}
