using HardwareHero.Shared.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using System.Text;

namespace HardwareHero.Shared.Extensions
{
    public static class CommonServiceCollectionExtensions
    {
        public static void ConfigureCommonOpenTelemetry(
            this IServiceCollection services,
            string OpenRemoteManageMeterName,
            string meter,
            string endpoint)
        {
            services.AddOpenTelemetry()
                .WithMetrics(opt =>
                    opt
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{OpenRemoteManageMeterName}.GatewayAPI"))
                        .AddMeter(meter)
                        .AddAspNetCoreInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation()
                        .AddOtlpExporter(opts =>
                        {
                            opts.Endpoint = new Uri(endpoint);
                        })
                );
        }

        public static void ConfigureCommonJwtAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

            var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
            var jwtCookieKey = builder.Configuration["JwtConfig:JwtCookieKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[jwtCookieKey ?? "token"];

                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void ConfigureCommonSQLServerContext<TContext>(
            this IServiceCollection services, WebApplicationBuilder builder, string connectionName) 
            where TContext : DbContext 
        {
            services.AddDbContext<TContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString(connectionName)));
        }
    }
}
