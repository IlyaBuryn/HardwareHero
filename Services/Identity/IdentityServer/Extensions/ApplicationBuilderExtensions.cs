using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>()
                    .Database.Migrate();

                serviceScope.ServiceProvider.GetService<ConfigurationDbContext>()
                    .Database.Migrate();
            }
        }
    }
}
