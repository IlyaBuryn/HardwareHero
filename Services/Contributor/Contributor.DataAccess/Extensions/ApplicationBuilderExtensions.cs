using Microsoft.AspNetCore.Builder;

namespace Contributor.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            var scriptPath = "/src/Services/Contributor/Contributor.DataAccess/Scripts";
            await app.InitDatabaseWithScriptsAsync<ContributorDbContext>(scriptPath);
        }
    }
}
