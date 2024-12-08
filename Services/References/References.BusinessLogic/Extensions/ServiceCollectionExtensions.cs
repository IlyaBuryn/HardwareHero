using FluentValidation;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.DependencyInjection;
using References.BusinessLogic.Contracts;
using References.BusinessLogic.Data;
using References.BusinessLogic.Data.Init;
using References.BusinessLogic.MapProfiles;
using References.BusinessLogic.Models;
using References.BusinessLogic.Services;
using System.Reflection;

namespace References.BusinessLogic.Extensions
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
                .AddMongoDbContext<ReferencesDbContext>();

            return services;
        }

        internal static IServiceCollection ConfigureServices(
            this IServiceCollection services)
        {
            services
                .AddScoped<ICurrencyService, CurrencyService>()
                .AddScoped<IRegionService, RegionService>()
                .AddScoped<IReferencesService, ReferencesService>();

            return services;
        }

        internal static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>))
                .AddScoped(typeof(ISeedRepositoryAsync<>), typeof(MongoSeedRepositoryAsync<>));

            return services;
        }

        internal static IServiceCollection ConfigureMapProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ReferencesMapProfile>();
            });

            return services;
        }

        internal static IServiceCollection ConfigureDtoValidators(
            this IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("References.DTOs"));
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
