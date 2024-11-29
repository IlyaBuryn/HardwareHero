using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.Mongo;
using Mail.BusinessLogic.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.BusinessLogic.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection ConfigureBusinessLayer(
            this IServiceCollection services)
        {
            services
                .ConfigureDbContext()
                .ConfigureRepositories()
                .ConfigureServices();

            return services;
        }

        internal static IServiceCollection ConfigureDbContext(
            this IServiceCollection services)
        {
            services
                .AddMongoDbContext<MailEventDbContext>();

            return services;
        }

        internal static IServiceCollection ConfigureServices(
            this IServiceCollection services)
        {
            services
                .AddScoped<IMailServicePresets, MailServicePresets>()
                .AddScoped<IMailService, MailService>();

            return services;
        }

        internal static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IBaseRepositoryAsync<>), typeof(MongoBaseRepositoryAsync<>));

            return services;
        }
    }
}
