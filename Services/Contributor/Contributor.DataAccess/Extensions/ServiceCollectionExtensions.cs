using Microsoft.Extensions.DependencyInjection;

namespace Contributor.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<ContributorDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(ICollectionRepositoryAsync<>), typeof(EFCollectionRepositoryAsync<>));
            builder.AddScoped(typeof(ICrudRepositoryAsync<>), typeof(EFCrudRepositoryAsync<>));
            builder.AddScoped(typeof(IValidationRepository<>), typeof(EFValidationRepository<>));
            builder.AddScoped(typeof(IObjectImageRepositoryAsync<>), typeof(ObjectImageRepositoryAsync<>));
            builder.AddScoped(typeof(IImageRepositoryAsync), typeof(ImageRepositoryAsync));

            builder.AddScoped<DbContext, ContributorDbContext>();
        }
    }
}
