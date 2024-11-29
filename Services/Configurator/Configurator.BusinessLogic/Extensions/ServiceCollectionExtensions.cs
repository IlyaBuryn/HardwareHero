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
        public static IServiceCollection ConfigureBusinessLayer(
            this IServiceCollection services)
        {
            services
                .ConfigureDbContext()
                .ConfigureServices()
                .ConfigureRepositories()
                .ConfigureMapProfiles()
                .ConfigureDtoValidators();

            return services;
        }

        internal static IServiceCollection ConfigureDbContext(
            this IServiceCollection services)
        {
            services
                .AddMongoDbContext<ConfiguratorDbContext>();

            return services;
        }

        internal static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));

            return services;
        }

        internal static IServiceCollection ConfigureServices(
            this IServiceCollection service)
        {
            service
                .AddScoped<IAssemblyService, AssemblyService>()
                .AddScoped<IConfiguratorService, ConfiguratorService>()
                .AddScoped<IDataService, DataService>()
                .AddScoped<IAttributeService, AttributeService>();

            return service;
        }

        internal static IServiceCollection ConfigureMapProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ConfiguratorMapProfile>();
            });

            return services;
        }

        internal static IServiceCollection ConfigureDtoValidators(
            this IServiceCollection service)
        {
            var assembly = Assembly.Load(new AssemblyName("Configurator.DTOs"));
            service.AddValidatorsFromAssembly(assembly);

            return service;
        }
    }
}
