using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
