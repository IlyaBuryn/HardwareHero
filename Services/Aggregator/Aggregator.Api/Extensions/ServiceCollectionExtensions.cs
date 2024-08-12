using Confluent.Kafka;
using EventStream.EventHandling;
using EventStream.Topics;
using FluentValidation.AspNetCore;
using KafkaEventStream.EventHandling;
using KafkaEventStream;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using KafkaEventStream.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.OpenApi;
using Microsoft.OpenApi.Models;

namespace Aggregator.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }

        public static void ConfigurePolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityConstants.ServicesApiScope);
                });
            });
        }

        public static void ConfigureOpenTelemetry(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonOpenTelemetry(
                "AggregatorRemoteManage",
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

        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(options =>
            {
                configuration.GetSection(typeof(T).Name).Bind(options);
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator.Api", Version = "v1" });
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
