using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDataAccessLayer(
            this IServiceCollection services, string connectionString)
        {
            services
                .ConfigureDbContext(connectionString)
                .ConfigureRepositories();

            return services;
        }

        internal static IServiceCollection ConfigureDbContext(
            this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<AggregatorDbContext>(options =>
                    options.UseSqlServer(connectionString))
                .AddScoped<DbContext, AggregatorDbContext>();

            return services;
        }

        internal static IServiceCollection ConfigureRepositories(
            this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IQueryRepositoryAsync<>), typeof(EFQueryRepositoryAsync<>))
                .AddScoped(typeof(IBaseRepositoryAsync<>), typeof(EFBaseRepositoryAsync<>))
                .AddScoped(typeof(ISpecificRepositoryAsync<>), typeof(EFSpecificRepositoryAsync<>));

            return services;
        }
    }
}
