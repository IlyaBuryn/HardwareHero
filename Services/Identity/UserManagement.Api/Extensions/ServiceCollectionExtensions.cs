using HardwareHero.Shared.Constants;
using HardwareHero.Shared.Data;
using HardwareHero.Shared.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerConstants.AuthenticationScheme)
                .AddJwtBearer(IdentityServerConstants.AuthenticationScheme, options =>
                {
                    options.Authority = IdentityServerConstants.IdentityServerAuthority;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }

        public static void AddIdentityServerAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityClientConstants.ServicesApiScope);
                });
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionNames.UsersConnection)));
        }
    }
}
