using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroApp.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
	private readonly ILogger<IndexModel> _logger = logger;

	public void OnGet()
	{
		_logger.LogInformation("Index page visited at {Time}", DateTime.UtcNow);
	}
}
