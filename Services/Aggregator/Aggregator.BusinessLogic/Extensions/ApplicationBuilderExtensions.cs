using Aggregator.DataAccess.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void DatabaseInitialization(this IApplicationBuilder app)
        {
            app.MigrationInitialization();
        }
    }
}
