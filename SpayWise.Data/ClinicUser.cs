using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpayWise.Data.Conventions;

namespace SpayWise.Data;

[Flags]
public enum Permissions
{	
	ManageUsers = 1 << 0,
	ManageClinic = 1 << 1,
	ManageCapacity = 1 << 2,
	ManageItems = 1 << 3,
	EditAppointments = 1 << 4,
	EditMedical = 1 << 5,
	PostInvoices = 1 << 6,
	ViewReports = 1 << 7,
	ManageVolumeClients = 1 << 8,
	All = ~0
}

public class ClinicUser : BaseTable
{
	public int ClinicId { get; set; }
	public int UserId { get; set; }	
	public bool IsEnabled { get; set; } = true;
	public int[] VolumeClientIds { get; set; } = [];
	public Permissions Permissions { get; set; } = Permissions.EditAppointments;

	public Clinic? Clinic { get; set; }	
	public ApplicationUser? ApplicationUser { get; set; }	
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

		builder.HasOne(cu => cu.ApplicationUser)
			.WithMany(e => e.Clinics)
			.HasForeignKey(cu => cu.UserId)
			.HasPrincipalKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}