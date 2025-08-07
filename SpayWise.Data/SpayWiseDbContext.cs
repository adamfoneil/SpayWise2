using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpayWise.Data;

public class SpayWiseDbContext(DbContextOptions<SpayWiseDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

}
