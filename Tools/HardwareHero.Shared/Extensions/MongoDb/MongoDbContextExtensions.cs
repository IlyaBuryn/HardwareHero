using HardwareHero.Shared.Options;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HardwareHero.Shared.Extensions.MongoDb
{
    public static class MongoDbContextExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(
            this IServiceCollection services)
        where TContext : MongoDbContext
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddScoped<MongoDbContext, TContext>();

            return services;
        }

        public static DatabaseOptions GetMongoDatabaseOptions(
            this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            return options;
        }
    }
}
