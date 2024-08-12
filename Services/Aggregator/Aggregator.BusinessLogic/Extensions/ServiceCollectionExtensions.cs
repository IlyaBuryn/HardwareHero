using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder, string connectionString)
        {
            ConfigureServices(builder);
            ConfigureMapProfiles(builder);
            ConfigureDtoValidators(builder);

            builder.ConfigureDataAccessLayer(connectionString);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IComponentService, ComponentService>();
            service.AddScoped<IComponentTypeService, ComponentTypeService>();
            service.AddScoped<IComponentAttributesService, ComponentAttributesService>();
            service.AddScoped<IComponentReviewService, ComponentReviewService>();
            service.AddScoped<IComponentImagesService, ComponentImagesService>();

            service.AddScoped<IMaintenanceService, MaintenanceService>();
            service.AddScoped<IMaintenanceTypeService, MaintenanceTypeService>();
            service.AddScoped<IMaintenanceReviewService, MaintenanceReviewService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AggregatorMapProfile>();
            });
        }

        private static void ConfigureDtoValidators(IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("HardwareHero.Shared"));
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}
