using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aggregator.DataAccess.Data;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Repositories.EF;
using HardwareHero.Services.Shared.Repositories.Generic;

namespace Aggregator.DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<AggregatorDbContext>(options => 
                options.UseSqlServer(connectionString));

            builder.AddScoped(typeof(ICollectionRepositoryAsync<>), typeof(EFCollectionRepositoryAsync<>));
            builder.AddScoped(typeof(ICrudRepositoryAsync<>), typeof(EFCrudRepositoryAsync<>));
            builder.AddScoped(typeof(IValidationRepository<>), typeof(EFValidationRepository<>));
            builder.AddScoped(typeof(IIMagesRepositoryAsync<>), typeof(ImagesRepositoryAsync<>));

            builder.AddScoped<DbContext, AggregatorDbContext>();
        }
    }
}
