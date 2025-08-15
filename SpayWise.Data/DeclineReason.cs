using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class DeclineReason : BaseTable
{
	public int ClinicId { get; set; }
	public string Name { get; set; } = default!;
	/// <summary>
	/// text to display if this reason is selected by a 
	/// ScreeningQuestion to explain why the appointment request is denied        
	/// </summary>
	public string ClientDescription { get; set; } = default!;

	/// <summary>
	/// do we suggest client reschedule after being declined
	/// </summary>
	public bool IsRescheduleSuggested { get; set; }

	public bool IsActive { get; set; } = true;

	public Clinic? Clinic { get; set; }
}

public class DeclineReasonConfiguration : IEntityTypeConfiguration<DeclineReason>
{
	public void Configure(EntityTypeBuilder<DeclineReason> builder)
	{
		builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
		builder.Property(e => e.ClientDescription).HasMaxLength(255).IsRequired();
		builder.HasIndex(e => new { e.ClinicId, e.Name }).IsUnique();
		builder.HasOne(e => e.Clinic).WithMany(c => c.DeclineReasons).HasForeignKey(e => e.ClinicId).OnDelete(DeleteBehavior.Cascade);
	}
}
