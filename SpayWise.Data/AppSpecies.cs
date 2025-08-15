using SpayWise.Data.Conventions;

namespace SpayWise.Data;

/// <summary>
/// defines Dog and Cat to the application
/// </summary>
public class AppSpecies : BaseTable
{
	public string Name { get; set; } = default!;
}
