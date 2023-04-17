using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aggregator.DataAccess.Data;
using Aggregator.DataAccess.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Aggregator.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<AggregatorDbContext>(options => 
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(IPageRepositoryAsync<>), typeof(AggregatorEFRepository<>));

            builder.AddScoped<DbContext, AggregatorDbContext>();
        }
    }
}
