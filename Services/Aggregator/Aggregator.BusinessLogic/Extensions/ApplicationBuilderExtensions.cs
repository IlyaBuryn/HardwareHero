using Aggregator.DataAccess.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task DatabaseInitialization(this IApplicationBuilder app)
        {
            await app.MigrationInitialization();
        }
    }
}
