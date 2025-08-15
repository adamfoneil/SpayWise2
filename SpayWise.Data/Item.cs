using SpayWise.Data.Conventions;

namespace SpayWise.Data;

public class Item : BaseTable
{
	public int ClinicId { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public string? DisplayCategory { get; set; }
	/// <summary>
	/// null means it cannot be invoiced (internal/surgical items) and does not require client Id
	/// </summary>
	public decimal? Price { get; set; }
	public decimal? OnlinePrice { get; set; }
	/// <summary>
	/// clients can select qty
	/// </summary>
	public bool HasPurchaseQuantity { get; set; }
	/// <summary>
	/// qty affects price
	/// </summary>
	public bool PricedByQuantity { get; set; }

	public int? DiscountMinQty { get; set; }
	public int? RewardQty { get; set; }

	// filters
	public int? AppSpeciesId { get; set; }
	public int? SexId { get; set; }
	public int? MinWeight { get; set; }
	public int? MaxWeight { get; set; }

	// medical
	public int? MedicalCategoryId { get; set; }
	public bool HasDosage { get; set; }
	public string? DosageUnits { get; set; }
	public bool ShowDosageOnSummary { get; set; }
	public bool AllowDosingWeight { get; set; }
	public string? MedicalCode { get; set; } // for filtering on medical page
	public string? Concentrations { get; set; } // for "cocktail" items, what's in it?
	public string? Instructions { get; set; } // prescription instructions
	public int? DefaultRouteId { get; set; }
	public int[] AllowRouteIds { get; set; } = [];

	public bool IsActive { get; set; } = true;

	public Clinic? Clinic { get; set; }
}
