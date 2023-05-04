using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Prices.BusinessLogic.Contracts;
using Prices.BusinessLogic.MapProfiles;
using Prices.BusinessLogic.Services;
using System.Reflection;

namespace Prices.BusinessLogic.Extensions
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
            service.AddScoped<IContributorPricesService, ContributorPriceService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ContributorPricesMapProfile>();
            });
        }

        private static void ConfigureDtoValidators(IServiceCollection service)
        {
            var assembly = Assembly.Load(new AssemblyName("HardwareHero.Services.Shared"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
