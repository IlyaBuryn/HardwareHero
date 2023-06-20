using HardwareHero.Services.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<UsersDbContext>()!
                    .Database.Migrate();
            }
        }
    }
}
