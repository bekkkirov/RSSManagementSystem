using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSS.Infrastructure.Identity.Entities;

namespace RSS.Infrastructure.Identity;

public class IdentityContext : IdentityDbContext<UserIdentity, UserRole, int>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }
}
