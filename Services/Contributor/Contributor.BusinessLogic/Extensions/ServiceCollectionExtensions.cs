using Microsoft.Extensions.DependencyInjection;
using Contributor.DataAccess.Extensions;
using Contributor.BusinessLogic.Contracts;
using Contributor.BusinessLogic.Services;
using Contributor.BusinessLogic.MappingProfiles;
using System.Reflection;
using FluentValidation;

namespace Contributor.BusinessLogic.Extensions
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
            service.AddScoped<IContributorService, ContributorService>();
            service.AddScoped<IChatService, ChatService>();
            service.AddScoped<IContributorExcellenceService, ContributorExcellenceService>();
            service.AddScoped<ISubscriptionService, SubscriptionService>();
            service.AddScoped<IReferencesDataService, ReferencesDataService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ContributorMapProfile>();
            });
        }

        private static void ConfigureDtoValidators(IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("HardwareHero.Services.Shared"));
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}
