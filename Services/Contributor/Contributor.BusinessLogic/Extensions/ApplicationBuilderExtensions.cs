using Microsoft.AspNetCore.Builder;
using Contributor.DataAccess.Extensions;

namespace Contributor.BusinessLogic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void DatabaseInitialization(this IApplicationBuilder app)
        {
            app.MigrationInitialization();
        }
    }
}
