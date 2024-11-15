using HardwareHero.Shared.Options;
using HardwareHero.Shared.Repositories.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HardwareHero.Shared.Extensions.MongoDb
{
    public static class MongoDbContextExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(
            this IServiceCollection services,
            string? connectionString,
            string? databaseName)
        where TContext : MongoDbContext
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            ArgumentNullException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));

            services.AddSingleton(provider =>
            {
                var context = Activator.CreateInstance<TContext>();
                context.Initialize(connectionString, databaseName);
                return context;
            });

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
