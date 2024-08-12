using Identity.Api.Data;

namespace Identity.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public async static Task UseDatabaseSeeding(this IApplicationBuilder app, string[] args)
        {
            var seedEnvironmentVariable = Environment.GetEnvironmentVariable("SEED");
            var isSeed = seedEnvironmentVariable != null && seedEnvironmentVariable.Contains("/seed");

            if (!isSeed)
            {
                isSeed = args.Contains("/seed");
            }

            if (isSeed)
            {
                using (var scope = app.ApplicationServices.CreateScope()) // was app.Services
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var defaultDataSeed = services.GetRequiredService<DefaultDataSeed>();
                        await defaultDataSeed.EnsureSeedData();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while retrieving the service.");
                    }
                }
            }
        }
    }
}
