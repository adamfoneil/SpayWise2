using Hydro;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroApp.Pages.Components;

public enum MatchType
{
    Folder,
    FullPath
}

public class NavItem : HydroComponent
{
    public string Text { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }    
	public MatchType MatchType { get; set; } = MatchType.Folder;

    public string? ActiveClass => ViewContext.RouteData.Values["page"] is string page
        ? MatchType switch
        {
            MatchType.Folder => GetFirstSegment(Href).Equals(GetFirstSegment(page), StringComparison.OrdinalIgnoreCase) ? "active" : null,
            MatchType.FullPath => page.Equals(Href, StringComparison.OrdinalIgnoreCase) ? "active" : null,            
            _ => null
		}
        : null;

    private static string GetFirstSegment(string page)
    {
		// Ensure the path starts with a slash for Uri parsing
		var uri = new Uri("http://dummy" + (page.StartsWith("/") ? page : "/" + page));
		return uri.Segments.Length > 1
			? uri.Segments[1].TrimEnd('/')
			: uri.Segments[0].TrimEnd('/');
	}
}
