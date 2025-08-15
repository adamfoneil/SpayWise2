using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class VolumeClient : BaseTable
{
	public int ClinicId { get; set; }
	public string Code { get; set; } = default!;
	public int ClientId { get; set; }
	public string? BackColor { get; set; }
	public string? TextColor { get; set; }
	public string? Category { get; set; }

	public bool AllowInvoicing { get; set; }
	public bool RequireCredit { get; set; }
	public bool AllowOnlineScheduling { get; set; }
	public bool AllowWeightSurcharges { get; set; }

	public Clinic? Clinic { get; set; }
	public Client? Client { get; set; }
}
