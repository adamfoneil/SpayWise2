using Hydro.Configuration;
using Microsoft.EntityFrameworkCore;
using SpayWise.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = AppDbFactory.GetConnectionString(builder.Configuration, args);
builder.Services.AddDbContextFactory<SpayWiseDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Singleton);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();
builder.Services.AddHydro();

builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)	
	.AddEntityFrameworkStores<SpayWiseDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseAuthentication();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.UseHydro();

app.Run();
