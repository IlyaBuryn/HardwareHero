using HardwareHero.Services.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupUserContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(connectionString));
        }
    }
}
