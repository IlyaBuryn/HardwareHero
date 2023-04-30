using Contributor.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contributor.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ContributorDbContext>()
                    .Database.Migrate();
            }
        }
    }
}
