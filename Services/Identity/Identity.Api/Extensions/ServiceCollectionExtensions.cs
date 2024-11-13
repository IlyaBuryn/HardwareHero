using Identity.Api.Data;
using Identity.Shared.Contexts;
using FluentValidation.AspNetCore;
using Identity.Api.Contracts;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using HardwareHero.Shared.OpenApi;
using Identity.Shared.Domain;

namespace Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }

        public static void ConfigureOpenTelemetry(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonOpenTelemetry(
                "IdentityRemoteManage",
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

        public static void ConfigureSQLServerContexts(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.ConfigureCommonSQLServerContext<GrantsDbContext>(builder, ConnectionNames.IdentityServerConnection);
            services.ConfigureCommonSQLServerContext<UsersDbContext>(builder, ConnectionNames.UsersConnection);
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClaimsService, ClaimsService>();

            services.AddScoped<DefaultDataSeed>();
        }

        public static void ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<TokenCleanupService>();
        }

        public static void ConfigurePolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", HardwareHero.Shared.Constants.IdentityConstants.ServicesApiScope);
                });
            });
        }

        public static void ConfigureCustomIdentity(this IServiceCollection services)
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

        public static void ConfigureSwagger(this IServiceCollection services)
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
