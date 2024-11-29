using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBusinessLayer(
            this IServiceCollection services, string connectionString)
        {
            services
                .ConfigureBusinessServices()
                .ConfigureMapProfiles()
                .ConfigureDtoValidators();

            services
                .ConfigureDataAccessLayer(connectionString);

            return services;
        }

        internal static IServiceCollection ConfigureBusinessServices(
            this IServiceCollection services)
        {
            services
                .AddScoped<IComponentService, ComponentService>()
                .AddScoped<IComponentTypeService, ComponentTypeService>()
                .AddScoped<IComponentAttributesService, ComponentAttributesService>()
                .AddScoped<IComponentReviewService, ComponentReviewService>()
                .AddScoped<IComponentImagesService, ComponentImagesService>()

                .AddScoped<ISpecificationService, SpecificationService>();

            return services;
        }

        internal static IServiceCollection ConfigureMapProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AggregatorMapProfile>();
            });

            return services;
        }

        internal static IServiceCollection ConfigureDtoValidators(
            this IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("Aggregator.DTOs"));
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
