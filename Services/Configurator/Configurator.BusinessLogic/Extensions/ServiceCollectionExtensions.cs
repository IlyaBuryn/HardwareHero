using Configurator.BusinessLogic.Contracts;
using Configurator.BusinessLogic.MapProfiles;
using Configurator.BusinessLogic.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Configurator.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder)
        {
            ConfigureServices(builder);
            ConfigureMapProfiles(builder);
            ConfigureDtoValidators(builder);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IAssemblyService, AssemblyService>();
            service.AddScoped<IComponentTypesService, ComponentTypesService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ConfiguratorMapProfile>();
            });
        }

        private static void ConfigureDtoValidators(IServiceCollection service)
        {
            var assembly = Assembly.Load(new AssemblyName("HardwareHero.Services.Shared"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
