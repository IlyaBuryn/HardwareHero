using Microsoft.AspNetCore.Builder;

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
