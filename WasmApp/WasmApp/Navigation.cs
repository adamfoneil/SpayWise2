namespace WasmApp;

public static class Navigation
{
	public static MenuItem[] Menu =>
	[
		new("Appointments", "/Appointments/Index") { Children =
		[
			new("Intake", "/Appointments/Intake"),
			new("Outtake", "/Appointments/Outtake"),
			new("Requested", "/Appointments/Requested"),
			new("All", "/Appointments/All"),
			new("Create", "/Appointments/Create"),
			new("Online", "/Appointments/Online")
		]},
		new("Medical", "/Medical/Index"),
		new("Calendar", "/Calendar/Index"),
		new("Clients", "/Clients/Index"),
		new("Invoice", "/Invoice/Index"),
		new("Reports", "/Reports/Index"),
		new("Setup", "/Setup/Index"),
		new("Help", "/Help/Index")
	];
}

public class MenuItem(string Text, string Href)
{
	public string Text { get; init; } = Text;
	public string Href { get; init; } = Href;
	public MenuItem[] Children { get; init; } = [];
}