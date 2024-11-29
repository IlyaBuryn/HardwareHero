using Configurator.BusinessLogic.Models;
using MongoDB.Bson.Serialization.Conventions;

namespace Configurator.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<WebApplication> SetupDefaultDataAsync(
            this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IDataService>();

                //await config.EnsureDatabaseFromFileAsync<ConfiguratorComponent>(
                //    "/src/Services/Configurator/Configurator.BusinessLogic/config/config.data.json", ConfiguratorCollectionNames.ComponentsCollection);

                //await config.EnsureDatabaseFromFileAsync<ConfiguratorRule>(
                //    "/src/Services/Configurator/Configurator.BusinessLogic/config/config.rules.json", ConfiguratorCollectionNames.ConfiguratorRulesCollection);
            }

            return app;
        }
    }
}
