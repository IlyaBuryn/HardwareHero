using Aggregator.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AggregatorDbContext>()!
                    .Database.Migrate();
            }
        }
    }
}
