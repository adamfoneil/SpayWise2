using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class Clinic : BaseTable
{
	public string Name { get; set; } = default!;
	public string Address1 { get; set; } = default!;
	public string? Address2 { get; set; }
	public string City { get; set; } = null!;
	public string State { get; set; } = null!;
	public string ZipCode { get; set; } = null!;
	public string PrimaryPhone { get; set; } = null!;
	public string? EmergencyPhone { get; set; }
	public string? FaxNumber { get; set; }
	public string Email { get; set; } = null!;
	public string? WebsiteUrl { get; set; }
	public string? TimeZoneId { get; set; }
	public decimal SalesTaxRate { get; set; } = 0.0M;
	public int OwnerUserId { get; set; }
	public bool IsActive { get; set; } = true;

	public ICollection<ClinicUser> Users { get; set; } = [];
	public ICollection<Species> Species { get; set; } = [];
	public ICollection<DeclineReason> DeclineReasons { get; set; } = [];
	public ICollection<Item> Items { get; set; } = [];
	public ICollection<Location> Locations { get; set; } = [];
}
