using HardwareHero.Shared.Extensions.Scripts;
using Microsoft.AspNetCore.Builder;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            await app.InitDatabaseWithScriptsAsync<AggregatorDbContext>();
        }
    }
}
