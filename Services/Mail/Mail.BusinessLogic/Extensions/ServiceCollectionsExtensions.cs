using FluentValidation;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Mail.BusinessLogic.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mail.BusinessLogic.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection service)
        {
            ConfigureServices(service);
            ConfigureMapProfiles(service);
            ConfigureDtoValidators(service);
        }

        public static void ConfigureDbContext(this IServiceCollection service, DatabaseOptions options)
        {
            service.AddMongoDbContext<MailDbContext>(options.ConnectionString, options.DatabaseName);
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            //service.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));

            service.AddScoped<IMailServicePresets, MailServicePresets>();
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
            var assembly = Assembly.Load(new AssemblyName("Mail.DTOs"));
            service.AddValidatorsFromAssembly(assembly);
        }
    }
}
