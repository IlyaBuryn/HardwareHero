using Microsoft.AspNetCore.Builder;
using Contributor.DataAccess.Extensions;

namespace Contributor.BusinessLogic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseInitialization(this IApplicationBuilder app)
        {
            await DataAccess.Extensions
                .ApplicationBuilderExtensions.DatabaseInitialization(app);
        }
    }
}
