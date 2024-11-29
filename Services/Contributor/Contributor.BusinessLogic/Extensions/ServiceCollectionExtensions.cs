using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Contributor.BusinessLogic.Extensions
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
                .AddScoped<IContributorService, ContributorService>()
                .AddScoped<IChatService, ChatService>()
                .AddScoped<IContributorExcellenceService, ContributorExcellenceService>()
                .AddScoped<ISubscriptionService, SubscriptionService>()
                .AddScoped<IReferencesDataService, ReferencesDataService>();

            return services;
        }

        internal static IServiceCollection ConfigureMapProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ContributorMapProfile>();
            });

            return services;
        }

        internal static IServiceCollection ConfigureDtoValidators(
            this IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("Contributor.DTOs"));
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
