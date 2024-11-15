using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prices.BusinessLogic.Data;
using System.Reflection;

namespace Prices.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection service)
        {
            ConfigureServices(service);
            ConfigureMapProfiles(service);
            ConfigureDtoValidators(service);
        }

        public static void ConfigureDbContext(this IServiceCollection service, DatabaseOptions options)
        {
            service.AddMongoDbContext<PricesDbContext>(options.ConnectionString, options.DatabaseName);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));

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
            var assembly = Assembly.Load(new AssemblyName("Prices.DTOs"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
