using FluentValidation;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace Mail.BusinessLogic.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection service)
        {
            ConfigureRepositories(service);
            ConfigureServices(service);
            ConfigureMapProfiles(service);
            ConfigureDtoValidators(service);
        }

        private static void ConfigureRepositories(IServiceCollection service)
        {
            service.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            service.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.Collections[ConfiguratorCollectionNames.MailCollection].CollectionName);
            });
            service.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            service.AddScoped<IMailService, MailService>();
            service.AddScoped<IMailServicePresets, MailServicePresets>();
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
