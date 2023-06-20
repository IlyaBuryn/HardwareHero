namespace Configurator.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task ConfigureDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<Config>();
                await config.SeedDatabaseAsync();
            }
        }
    }
}
