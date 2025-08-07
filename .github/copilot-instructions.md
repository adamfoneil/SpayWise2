# SpayWise2 Development Instructions

SpayWise2 is a .NET 9.0 web application using the Hydro framework, built as a reboot of Hs5 to avoid Blazor Server socket disconnect issues. The application uses PostgreSQL as the primary database with Entity Framework Core and ASP.NET Core Identity for authentication.

**ALWAYS follow these instructions first and only fall back to additional search and context gathering if the information here is incomplete or found to be in error.**

## Working Effectively

### Prerequisites and Environment Setup
- **CRITICAL**: Install .NET 9.0 SDK first - the project targets .NET 9.0:
  - `curl -sSL https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh | bash -s -- --version 9.0.103 --install-dir /usr/share/dotnet`
  - `export PATH="/usr/share/dotnet:$PATH"`
  - `export DOTNET_ROOT="/usr/share/dotnet"`
  - Verify with: `dotnet --version` (should show 9.0.103 or higher)
- Install Entity Framework Core tools globally:
  - `dotnet tool install --global dotnet-ef`
  - Add tools to PATH: `export PATH="$HOME/.dotnet/tools:$PATH"`

### Build and Setup Commands
- **NEVER CANCEL builds or long-running commands** - they may take 45+ minutes in some environments
- **Package restore**: `dotnet restore`
  - Takes approximately 49 seconds. NEVER CANCEL. Set timeout to 90+ seconds minimum.
- **Build solution**: `dotnet build`
  - Takes approximately 22 seconds. NEVER CANCEL. Set timeout to 60+ seconds minimum.
- **Code formatting**: `dotnet format`
  - Always run this before committing changes or CI will fail
  - Verify formatting: `dotnet format --verify-no-changes`
- **Tests**: `dotnet test` 
  - Currently no tests exist in the solution

### Database Setup
- **PostgreSQL is required** - the application uses PostgreSQL as primary database
- **Default connection string** (in appsettings.json):
  - Host: localhost
  - Port: 5433 (non-standard port)
  - Database: SpayWise
  - Username: postgresUser
  - Password: postgresPwd
- **Database migrations**: 
  - From SpayWise.Data directory: `dotnet ef database update`
  - Takes approximately 10 seconds when database is properly configured
  - **CRITICAL**: Ensure PostgreSQL is running and connection string is correct before running migrations

### Running the Application
- **Start web application**: 
  - From HydroApp directory: `dotnet run`
  - Application starts on: http://localhost:5189 (HTTP) and https://localhost:7022 (HTTPS)
  - Startup takes approximately 5 seconds
- **NEVER CANCEL application startup** - wait for "Now listening on:" message
- **Always run database migrations before starting the application** if database schema changes

## Validation

### Manual Testing Requirements
- **ALWAYS test the running application** after making changes:
  - Navigate to http://localhost:5189
  - Verify the home page loads (should show "SpayWise" title)
  - Test basic navigation between pages (Index, Privacy)
  - Check for any console errors in browser developer tools
- **Database connectivity test**:
  - Run `dotnet ef database update` from SpayWise.Data directory
  - Should complete without errors if database is properly configured
- **Always run formatting check**: `dotnet format --verify-no-changes`
  - Must pass or CI will fail

### Build Validation Steps
1. `dotnet restore` (90s timeout minimum)
2. `dotnet build` (60s timeout minimum)  
3. `dotnet format --verify-no-changes`
4. From SpayWise.Data: `dotnet ef database update`
5. From HydroApp: `dotnet run` 
6. Navigate to http://localhost:5189 and verify application loads

## Common Tasks

### Project Structure
The solution contains two main projects:
- **SpayWise.Data**: Entity Framework models, DbContext, and migrations
- **HydroApp**: Web application using Hydro framework

### Key Directories and Files
```
SpayWise2/
├── SpayWise2.sln                    # Solution file
├── SpayWise.Data/                   # Data layer project
│   ├── SpayWise.Data.csproj        # Project file
│   ├── SpayWiseDbContext.cs        # Entity Framework DbContext
│   ├── Migrations/                  # Database migrations
│   └── [Model classes]             # Entity models (Clinic, Client, etc.)
├── HydroApp/                       # Web application project  
│   ├── HydroApp.csproj             # Project file
│   ├── Program.cs                  # Application entry point
│   ├── appsettings.json            # Configuration (connection strings)
│   ├── Pages/                      # Razor pages
│   └── wwwroot/                    # Static web assets
└── README.md                       # Project documentation
```

### Database Models
- **ApplicationUser**: ASP.NET Core Identity user
- **Clinic**: Clinic/veterinary practice information
- **ClinicUser**: Association between users and clinics
- **Client**: Pet owner/client information  
- **ClientPhone**: Client phone numbers

### Configuration Notes
- **Connection string**: Defined in HydroApp/appsettings.json under "ConnectionStrings.Local"
- **Launch settings**: HydroApp/Properties/launchSettings.json defines startup URLs
- **Database provider**: Uses Npgsql.EntityFrameworkCore.PostgreSQL for PostgreSQL

### Troubleshooting Common Issues
- **Build fails with .NET version error**: Install .NET 9.0 SDK (see Prerequisites)
- **Database connection fails**: Check PostgreSQL is running and connection string is correct
- **EF tools not found**: Install dotnet-ef tool and add to PATH
- **Formatting errors**: Run `dotnet format` to auto-fix formatting issues
- **Long build times**: This is normal - NEVER CANCEL, set appropriate timeouts

### Working with Entity Framework
- **Add migration**: `dotnet ef migrations add <MigrationName>`
- **Update database**: `dotnet ef database update`
- **Remove last migration**: `dotnet ef migrations remove`
- **All EF commands must be run from SpayWise.Data directory**

### Performance Expectations
| Command | Expected Time | Minimum Timeout |
|---------|---------------|-----------------|
| dotnet restore | 49s | 90s |
| dotnet build | 22s | 60s |
| dotnet ef database update | 10s | 30s |
| dotnet run (startup) | 5s | 30s |
| dotnet format | 15s | 60s |

**CRITICAL**: All timeouts above are minimums - in some environments these operations may take significantly longer. NEVER CANCEL operations, always wait for completion.