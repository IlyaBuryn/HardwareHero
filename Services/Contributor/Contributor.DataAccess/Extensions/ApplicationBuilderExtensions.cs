using Microsoft.AspNetCore.Builder;

namespace Contributor.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            await app.InitDatabaseWithScriptsAsync<ContributorDbContext>();
        }
    }
}
