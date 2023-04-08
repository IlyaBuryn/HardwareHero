using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Data.Configurations;

namespace HardwareHero.Services.Shared.Data
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new WishListComponentsConfiguration());
            builder.ApplyConfiguration(new WishListConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }
}
