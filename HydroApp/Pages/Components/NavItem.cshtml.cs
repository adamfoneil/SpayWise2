using Hydro;

namespace HydroApp.Pages.Components;

public enum MatchType
{
    StartsWith,
    EndsWith,
    Exact
}

public class NavItem : HydroComponent
{
    public string Text { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string MatchText { get; set; } = string.Empty;
	public MatchType MatchType { get; set; } = MatchType.StartsWith;

    public string? ActiveClass => ViewContext.RouteData.Values["page"] is string page
        ? MatchType switch
        {
            MatchType.StartsWith => page.StartsWith(MatchText, StringComparison.OrdinalIgnoreCase) ? "active" : null,
            MatchType.EndsWith => page.EndsWith(MatchText, StringComparison.OrdinalIgnoreCase) ? "active" : null,
            MatchType.Exact => string.Equals(page, MatchText, StringComparison.OrdinalIgnoreCase) ? "active" : null,
            _ => null
		}
        : null;
}
