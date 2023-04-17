using Configurator.BusinessLogic.Contracts;
using Configurator.BusinessLogic.MapProfiles;
using Configurator.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Configurator.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder)
        {
            ConfigureServices(builder);
            ConfigureMapProfiles(builder);
            //ConfigureDtoValidators(builder);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IAssemblyService, AssemblyService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ConfiguratorMapProfile>();
            });
        }
    }
}
