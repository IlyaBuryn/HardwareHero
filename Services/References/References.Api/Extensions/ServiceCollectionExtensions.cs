using EventDriven.Kafka.Config;
using EventDriven.Kafka.Extensions;
using FluentValidation.AspNetCore;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.Extensions.MongoDb;
using HardwareHero.Shared.OpenApi;
using HardwareHero.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using References.Api.Handlers;
using References.BusinessLogic.Data.Init;

namespace References.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public async static Task<IServiceCollection> ConfigureSeedDatabaseAsync(
            this IServiceCollection services)
        {
            await services.SeedMongoDbDatabaseAsync(new RegionInit());
            await services.SeedMongoDbDatabaseAsync(new CurrencyInit());

            return services;
        }

        public static IServiceCollection ConfigureFluentValidation(
            this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection ConfigureCustomControllers(
            this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            return services;
        }

        public static IServiceCollection ConfigureEventServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            var messageConfig = builder.Configuration.GetSection("MessageKafkaConfig").Get<KafkaConfig>();
            var eventConfig = builder.Configuration.GetSection("EventKafkaConfig").Get<KafkaConfig>();

            services
                .AddKafkaReplyService<CurrencyRequestMessage, CurrencyDto>(messageConfig)
                .AddKafkaReplyService<RegionRequestMessage, RegionDto>(messageConfig);

            return services;
        }

        public static IServiceCollection ConfigureEventHandlers(
            this IServiceCollection services)
        {
            services
                .AddHostedService<ReferencesEventsHandler>();

            return services;
        }

        public static IServiceCollection ConfigureOptions(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .ConfigureOptions<PageSizeOptions>(builder.Configuration);

            return services;
        }

        public static IServiceCollection ConfigurePolicyAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityConstants.ServicesApiScope);
                    policy.RequireRole("User");
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureOptions<T>(
            this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(options =>
            {
                configuration.GetSection(typeof(T).Name).Bind(options);
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "References.Api", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.OperationFilter<AuthResponsesOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection ConfigureCORSPolicy(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
