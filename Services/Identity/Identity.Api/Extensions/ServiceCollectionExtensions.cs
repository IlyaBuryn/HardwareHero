using Identity.Api.Data;
using Identity.Shared.Contexts;
using FluentValidation.AspNetCore;
using Identity.Api.Contracts;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using HardwareHero.Shared.OpenApi;
using Identity.Shared.Domain;
using EventDriven.Kafka.Config;
using EventDriven.Kafka.Extensions;
using Identity.Shared.Events;
using Mail.DTOs.Events;
using Identity.Api.Handlers;

namespace Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureFluentValidation(
            this IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("Identity.Shared"));
            services
                .AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            return services;
        }

        public static IServiceCollection ConfigureOpenTelemetry(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonOpenTelemetry(
                "IdentityRemoteManage",
                builder.Configuration.GetValue<string>("OpenRemoteManageMeterName"),
                builder.Configuration["Otel:Endpoint"]);

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

        public static IServiceCollection ConfigureSQLServerContexts(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .ConfigureCommonSQLServerContext<GrantsDbContext>
                    (builder, ConnectionNames.IdentityServerConnection)
                .ConfigureCommonSQLServerContext<UsersDbContext>
                    (builder, ConnectionNames.UsersConnection);

            return services;
        }

        public static IServiceCollection ConfigureEventServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            var messageConfig = builder.Configuration.GetSection("MessageKafkaConfig").Get<KafkaConfig>();
            var eventsConfig = builder.Configuration.GetSection("EventKafkaConfig").Get<KafkaConfig>();

            builder.Services
                .AddKafkaRequestService<CreateUserEvent, UserResultEvent>(messageConfig)
                .AddKafkaRequestService<UpdateUserEvent, UserResultEvent>(messageConfig)
                .AddKafkaRequestService<DeleteUserEvent, UserResultEvent>(messageConfig)

                .AddKafkaReplyService<TokenRequestEvent, AuthResultEvent>(messageConfig)

                .AddKafkaProducer<SendMailEvent>(eventsConfig);

            return services;
        }

        public static IServiceCollection ConfigureServices(
            this IServiceCollection services)
        {
            services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IClaimsService, ClaimsService>();

            return services;
        }

        public static IServiceCollection ConfigureEventHandlers(
            this IServiceCollection services)
        {
            services
                .AddHostedService<TokenEventsHandler>();

            return services;
        }

        public static IServiceCollection ConfigureBackgroundServices(
            this IServiceCollection services)
        {
            services
                .AddHostedService<TokenCleanupService>();

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
                    policy.RequireClaim("scope", HardwareHero.Shared.Constants.IdentityConstants.ServicesApiScope);
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureCustomIdentity(
            this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

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

        public static IServiceCollection ConfigureSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.Api", Version = "v1" });
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

        //public static void AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        //{
        //    services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

        //    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);

        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ValidateLifetime = false,
        //        RequireExpirationTime = false,
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    services.AddSingleton(tokenValidationParameters);

        //    services.AddAuthentication(options => {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(jwt => {
        //        jwt.SaveToken = true;
        //        jwt.TokenValidationParameters = tokenValidationParameters;
        //    });
        //}
    }
}
