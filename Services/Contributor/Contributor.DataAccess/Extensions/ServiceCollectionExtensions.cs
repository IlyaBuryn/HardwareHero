using Contributor.DataAccess.Data;
using Contributor.DataAccess.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contributor.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<ContributorDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(ICrudRepositoryAsync<>), typeof(ContributorEFRepository<>));

            builder.AddScoped<DbContext, ContributorDbContext>();
        }
    }
}
