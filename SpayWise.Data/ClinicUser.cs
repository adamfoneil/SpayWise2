using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpayWise.Data.Conventions;

namespace SpayWise.Data;

[Flags]
public enum Permissions
{	
	ManageUsers = 1 << 0,
	ManageCapacity = 1 << 1,
	ManageItems = 1 << 2,
	EditAppointments = 1 << 3,
	EditMedical = 1 << 4,
	PostInvoices = 1 << 5,
	ViewReports = 1 << 6
}

public class ClinicUser : BaseTable
{
	public int ClinicId { get; set; }
	public int UserId { get; set; }	
	public bool IsEnabled { get; set; } = true;
	public int[] VolumeClientIds { get; set; } = [];
	public Permissions Permissions { get; set; } = Permissions.EditAppointments;

	public Clinic? Clinic { get; set; }	
	public ApplicationUser? User { get; set; }	
}

public class ClinicUserConfiguration : IEntityTypeConfiguration<ClinicUser>
{
	public void Configure(EntityTypeBuilder<ClinicUser> builder)
	{
		builder.HasIndex(cu => new { cu.ClinicId, cu.UserId }).IsUnique();
		
		builder.HasOne(cu => cu.Clinic)
			.WithMany(c => c.Users)
			.HasForeignKey(cu => cu.ClinicId)			
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(cu => cu.User)
			.WithMany(e => e.Clinics)
			.HasForeignKey(cu => cu.UserId)
			.HasPrincipalKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}