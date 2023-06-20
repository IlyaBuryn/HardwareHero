using Configurator.BusinessLogic.Components.ComponentTypes;
using FluentValidation.AspNetCore;
using HardwareHero.Services.Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;

namespace Configurator.Api.Extensions
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

        public static void RegisterMongoClassMap(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<CPU>();
            BsonClassMap.RegisterClassMap<GPU>();
            BsonClassMap.RegisterClassMap<MB>();
            BsonClassMap.RegisterClassMap<SD>();
            BsonClassMap.RegisterClassMap<RAM>();
            BsonClassMap.RegisterClassMap<PS>();
            BsonClassMap.RegisterClassMap<Case>();
            BsonClassMap.RegisterClassMap<Cooler>();
        }

        public static void AddApiScopeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityClientConstants.ServicesApiScope);
                    policy.RequireRole("User");
                });
            });
        }

        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
        }

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }

        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string optionName) where T : class
        {
            services.Configure<T>(options =>
            {
                configuration.GetSection(optionName).Bind(options);
            });
        }

        public static void ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<Config>();
        }
    }
}
