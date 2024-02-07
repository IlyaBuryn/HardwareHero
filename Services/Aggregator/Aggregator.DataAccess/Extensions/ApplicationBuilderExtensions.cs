using Microsoft.AspNetCore.Builder;

namespace Aggregator.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            string scriptPath = "/src/Services/Aggregator/Aggregator.DataAccess/Scripts";
            await ScriptHelper.InitDatabaseWithSqlScript<AggregatorDbContext>(app, scriptPath);
        }
    }
}
