using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class Sex : BaseTable
{
	public string Name { get; set; } = default!;
	public string Letter { get; set; } = default!;
	public string ShortSterilizationIndicator { get; set; } = default!;
	public string LongSterilizationIndicator { get; set; } = default!;
}
