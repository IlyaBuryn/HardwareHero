using FluentValidation.AspNetCore;
using HardwareHero.Services.Shared.Constants;
using IdentityServer4.AccessTokenValidation;

namespace Prices.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;
                });
        }

        public static void AddApiScopeAuthorization(this IServiceCollection services)
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

        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .AddFluentValidation();
        }

        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string optionName) where T : class
        {
            services.Configure<T>(options =>
            {
                configuration.GetSection(optionName).Bind(options);
            });
        }
    }
}
