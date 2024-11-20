using Configurator.BusinessLogic.Data;
using FluentValidation;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
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

        public static void ConfigureDbContext(this IServiceCollection service, DatabaseOptions options)
        {
            service.AddMongoDbContext<ConfiguratorDbContext>(options.ConnectionString, options.DatabaseName);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));

            service.AddScoped<IAssemblyService, AssemblyService>();
            //service.AddScoped<IConfiguratorService, ConfiguratorService>();
            service.AddScoped<IDataService, DataService>();
            service.AddScoped<IAttributeService, AttributeService>();
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
            var assembly = Assembly.Load(new AssemblyName("Configurator.DTOs"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
