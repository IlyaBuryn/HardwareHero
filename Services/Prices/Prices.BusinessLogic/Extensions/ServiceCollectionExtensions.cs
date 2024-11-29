using FluentValidation;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Prices.BusinessLogic.Data;
using System.Reflection;

namespace Prices.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBusinessLayer(
            this IServiceCollection services)
        {
            services
                .ConfigureDbContext()
                .ConfigureRepositories()
                .ConfigureServices()
                .ConfigureMapProfiles()
                .ConfigureDtoValidators();

            return services;
        }

        internal static IServiceCollection ConfigureDbContext(
            this IServiceCollection services)
        {
            services
                .AddMongoDbContext<PricesDbContext>();

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
            this IServiceCollection services)
        {
            services
                .AddScoped<IContributorPricesService, ContributorPriceService>();

            return services;
        }

        internal static IServiceCollection ConfigureMapProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ContributorPricesMapProfile>();
            });

            return services;
        }

        internal static IServiceCollection ConfigureDtoValidators(
            this IServiceCollection service)
        {
            var assembly = Assembly.Load(new AssemblyName("Prices.DTOs"));
            service.AddValidatorsFromAssembly(assembly);

            return service;
        }
    }
}
