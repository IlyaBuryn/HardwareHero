using HardwareHero.Shared.Extensions.Scripts;
using Microsoft.AspNetCore.Builder;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> DatabaseInitialization(
            this IApplicationBuilder app)
        {
            var scriptPath = "/src/Services/Aggregator/Aggregator.DataAccess/Scripts";
            await app.InitDatabaseWithScriptsAsync<AggregatorDbContext>(scriptPath);

            return app;
        }
    }
}
