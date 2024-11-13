using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<AggregatorDbContext>(options => 
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(IQueryRepositoryAsync<>), typeof(EFQueryRepositoryAsync<>));
            builder.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(EFBaseRepositoryAsync<>));
            builder.AddScoped(typeof(ISpecificRepositoryAsync<>), typeof(EFSpecificRepositoryAsync<>));
            builder.AddScoped(typeof(IFileRepositoryAsync), typeof(GoogleDriveRepositoryAsync));

            builder.AddScoped<DbContext, AggregatorDbContext>();
        }
    }
}
