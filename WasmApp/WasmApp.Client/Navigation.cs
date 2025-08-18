namespace WasmApp.Client;

public static class Navigation
{
	public static MenuItem[] Menu =>
	[
		Appointments,
		new("Medical", "/Medical"),
		new("Calendar", "/Calendar/Index"),
		new("Clients", "/Clients/Index"),
		new("Invoice", "/Invoice/Index"),
		new("Reports", "/Reports/Index"),
		new("Setup", "/Setup/Index"),
		new("Help", "/Help/Index")
	];

	public static MenuItem Appointments => new("Appointments", "/Appointments")
	{
		Children =
		[
			new("Intake", "/Appointments/Intake"),
			new("Outtake", "/Appointments/Outtake"),
			new("Requested", "/Appointments/Requested"),
			new("All", "/Appointments/All"),
			new("Create", "/Appointments/Create"),
			new("Online", "/Appointments/Online")
		]
	};
}

public class MenuItem(string Text, string Href)
{
	public string Text { get; init; } = Text;
	public string Href { get; init; } = Href;
	public MenuItem[] Children { get; init; } = [];
}

/*
Calendar:
<nav-item href="/Calendar/SpayNeuter" icon="bi-scissors" text="Spay/Neuter" match-type="FullPath" />
<nav-item href="/Calendar/Wellness" icon="bi-heart" text="Wellness" match-type="FullPath" />

Clients
<nav-item href="/Clients/Volume" icon="bi-hourglass-split" text="Volume" match-type="FullPath" />
<nav-item href="/Clients/Owners" icon="bi-hourglass-split" text="Owners" match-type="FullPath" />

Invoice
<nav-item href="/Invoice/Pending" icon="bi-hourglass-split" text="Pending" match-type="FullPath" />
<nav-item href="/Invoice/AddCharges" icon="bi-plus-square" text="Add Charges" match-type="FullPath" />
<nav-item href="/Invoice/Receivable" icon="bi-currency-dollar" text="Receivable" match-type="FullPath" />

Setup
<nav-item href="/Setup/Clinic/Index" icon="bi-building" text="Clinic" match-type="FullPath" />
    <a asp-page="/Setup/Clinic/Users" class="list-group-item list-group-item-action">
        <i class="bi bi-people me-2"></i>Users
    </a>
    <a asp-page="/Setup/Clinic/Veterinarians" class="list-group-item list-group-item-action">
        <i class="bi bi-heart-pulse me-2"></i>Veterinarians
    </a>
    <a asp-page="/Setup/Clinic/DeclineReasons" class="list-group-item list-group-item-action">
        <i class="bi bi-x-circle me-2"></i>Decline Reasons
    </a>
    <a asp-page="/Setup/Clinic/Reminders" class="list-group-item list-group-item-action">
        <i class="bi bi-bell me-2"></i>Reminders
    </a>
    <a asp-page="/Setup/Clinic/PaymentMethods" class="list-group-item list-group-item-action">
        <i class="bi bi-credit-card me-2"></i>Payment Methods
    </a>
    <a asp-page="/Setup/Clinic/Logos" class="list-group-item list-group-item-action">
        <i class="bi bi-image me-2"></i>Logos
    </a>

<nav-item href="/Setup/Capacity/Index" icon="bi-speedometer2" text="Capacity" match-type="FullPath" />
	<a asp-page="/Setup/Capacity/Locations" class="list-group-item list-group-item-action">
        <i class="bi bi-geo-alt me-2"></i>Locations
    </a>
    <a asp-page="/Setup/Capacity/AppointmentTypes" class="list-group-item list-group-item-action">
        <i class="bi bi-calendar-event me-2"></i>Appointment Types
    </a>
    <a asp-page="/Setup/Capacity/Points" class="list-group-item list-group-item-action">
        <i class="bi bi-diagram-3 me-2"></i>Points
    </a>
    <a asp-page="/Setup/Capacity/Counts" class="list-group-item list-group-item-action">
        <i class="bi bi-bar-chart me-2"></i>Counts
    </a>
    <a asp-page="/Setup/Capacity/KennelSpace" class="list-group-item list-group-item-action">
        <i class="bi bi-house me-2"></i>Kennel Space
    </a>
    <a asp-page="/Setup/Capacity/AppointmentTimes" class="list-group-item list-group-item-action">
        <i class="bi bi-clock me-2"></i>Appointment Times
    </a>

<nav-item href="/Setup/Services/Index" icon="bi-tools" text="Services" match-type="FullPath" />
    <a asp-page="/Setup/Services/Items" class="list-group-item list-group-item-action">
        <i class="bi bi-list me-2"></i>Items
    </a>
    <a asp-page="/Setup/Services/VolumePricing" class="list-group-item list-group-item-action">
        <i class="bi bi-cash-stack me-2"></i>Volume Pricing
    </a>
    <a asp-page="/Setup/Services/Medical" class="list-group-item list-group-item-action">
        <i class="bi bi-bandaid me-2"></i>Medical
    </a>
    <a asp-page="/Setup/Services/Dosages" class="list-group-item list-group-item-action">
        <i class="bi bi-capsule me-2"></i>Dosages
    </a>

Help
<nav-item href="/Help/Learn" icon="bi-journal-text" text="Learn" match-type="FullPath" />
<nav-item href="/Help/Changelog" icon="bi-journal-text" text="Changelog" match-type="FullPath" />
<nav-item href="/Help/Forum" icon="bi-chat-dots" text="Forum" match-type="FullPath" />
*/