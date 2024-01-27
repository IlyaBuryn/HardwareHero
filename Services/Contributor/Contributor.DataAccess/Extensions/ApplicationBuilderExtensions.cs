using Contributor.DataAccess.Data;
using HardwareHero.Services.Shared.Helpers;
using Microsoft.AspNetCore.Builder;

namespace Contributor.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            string scriptPath = "/src/Services/Contributor/Contributor.DataAccess/Scripts";
            await ScriptHelper.InitDatabaseWithSqlScript<ContributorDbContext>(app, scriptPath);
        }
    }
}
