using Microsoft.AspNetCore.Builder;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task<IApplicationBuilder> DatabaseInitializationAsync(
            this IApplicationBuilder app)
        {
            await DataAccess.Extensions
                .ApplicationBuilderExtensions.DatabaseInitialization(app);

            return app;
        }
    }
}
