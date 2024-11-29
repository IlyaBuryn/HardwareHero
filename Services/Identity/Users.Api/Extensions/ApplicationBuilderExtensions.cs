using Users.Api.Data;

namespace Users.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMigration<TContext>
            (this IApplicationBuilder app, string migrationAssembly) where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();

                // Set the migration assembly
                var contextOptions = context.Database.GetDbConnection().ConnectionString;
                var optionsBuilder = new DbContextOptionsBuilder<TContext>();
                optionsBuilder.UseSqlServer(contextOptions, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly));

                // Create new context with migration assembly set
                var newContext = (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options)!;
                newContext.Database.Migrate();
            }

            return app;
        }

        public async static Task<IApplicationBuilder> SetupDefaultDataAsync(
            this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var defaultDataSeed = services.GetRequiredService<DefaultDataSeed>();
                    await defaultDataSeed.EnsureSeedDataAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while retrieving the service.");
                }
            }

            return app;
        }
    }
}
