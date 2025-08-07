using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class Client : BaseTable
{
	public int ClinicId { get; set; }
	/// <summary>
	/// last, first or org name
	/// </summary>
	public string Name { get; set; } = default!;
	public string? LastName { get; set; }
	public string? FirstName { get; set; }
	public string? Address { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public string? ZipCode { get; set; }
	public string? County { get; set; }
	public string? PrimaryPhone { get; set; }
	public string? EmergencyPhone { get; set; }
	public string? Email { get; set; }
	public string? Notes { get; set; }
	public bool IsRedFlag { get; set; }
	public decimal Balance { get; set; } = 0.0M;
	public bool AllowEmail { get; set; }
	public bool AllowSms { get; set; }

	public Clinic? Clinic { get; set; }
	public ICollection<ClientPhone> PhoneNumbers { get; set; } = [];
}
