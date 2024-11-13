using Microsoft.Extensions.DependencyInjection;

namespace Contributor.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<ContributorDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(IQueryRepositoryAsync<>), typeof(EFQueryRepositoryAsync<>));
            builder.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(EFBaseRepositoryAsync<>));
            builder.AddScoped(typeof(IFileRepositoryAsync), typeof(GoogleDriveRepositoryAsync));

            builder.AddScoped<DbContext, ContributorDbContext>();
        }
    }
}
