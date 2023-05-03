using FluentValidation;
using Mail.BusinessLogic.Contracts;
using Mail.BusinessLogic.MapProfiles;
using Mail.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mail.BusinessLogic.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder)
        {
            ConfigureServices(builder);
            ConfigureMapProfiles(builder);
            ConfigureDtoValidators(builder);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IMailService, MailService>();
        }

        private static void ConfigureMapProfiles(IServiceCollection service)
        {
            service.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MailMapProfile>();
            });
        }

        private static void ConfigureDtoValidators(IServiceCollection service)
        {
            var assembly = Assembly.Load(new AssemblyName("HardwareHero.Services.Shared"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
