using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class Species : BaseTable
{
	public int ClinicId { get; set; }
	public string Name { get; set; } = default!;
	public string BaseName { get; set; } = default!;
	public string Abbreviation { get; set; } = default!;
	public int AppSpeciesId { get; set; }
	public int? MinWeight { get; set; }
	public bool IsActive { get; set; } = true;
}
