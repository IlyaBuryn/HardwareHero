using Confluent.Kafka;
using EventStream.EventHandling;
using EventStream.Topics;
using FluentValidation.AspNetCore;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.OpenApi;
using KafkaEventStream;
using KafkaEventStream.EventHandling;
using KafkaEventStream.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Mail.Api.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void ConfigurePolicyAuthorization(this IServiceCollection services)
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
        }

        public static void ConfigureOpenTelemetry(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonOpenTelemetry(
                "MainRemoteManage",
                builder.Configuration.GetValue<string>("OpenRemoteManageMeterName"),
                builder.Configuration["Otel:Endpoint"]);
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

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mail.Api", Version = "v1" });
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
        }

        public static void ConfigureCORSPolicy(this IServiceCollection services)
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
        }
    }
}
