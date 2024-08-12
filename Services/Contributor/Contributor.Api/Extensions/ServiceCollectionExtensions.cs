using Confluent.Kafka;
using EventStream.EventHandling;
using EventStream.Topics;
using FluentValidation.AspNetCore;
using KafkaEventStream.EventHandling;
using KafkaEventStream.Extensions;
using KafkaEventStream;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Contributor.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityConstants.ServicesApiScope);
                });
            });
        }

        public static void ConfigureOpenTelemetry(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonOpenTelemetry(
                "ContributorRemoteManage",
                builder.Configuration.GetValue<string>("OpenRemoteManageMeterName"),
                builder.Configuration["Otel:Endpoint"]);
        }

        public static void AddApiScopeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityConstants.ServicesApiScope);
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

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contributor.Api", Version = "v1" });
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

        //public static void AddCustomControllers(this IServiceCollection services)
        //{
        //    services.AddControllers(options =>
        //    {
        //        options.SuppressAsyncSuffixInActionNames = false;
        //    })
        //    .AddJsonOptions(options =>
        //    {
        //        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //        options.JsonSerializerOptions.WriteIndented = true;
        //    });
        //}

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }

        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(options =>
            {
                configuration.GetSection(typeof(T).Name).Bind(options);
            });
        }
    }
}
