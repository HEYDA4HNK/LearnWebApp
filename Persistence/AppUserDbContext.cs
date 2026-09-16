using Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppUserDbContext : IdentityDbContext<AppUser>
{
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options)
        {
        }
}
