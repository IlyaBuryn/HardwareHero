using HardwareHero.Shared.Data;
using HardwareHero.Shared.Models.Users;
using IdentityServer.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupUserContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsersDbContext>(options =>
               options.UseSqlServer(connectionString));
        }

        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                 {
                     options.SignInScheme = Duende.IdentityServer.IdentityServerConstants.ExternalCookieAuthenticationScheme;
                     options.ClientId = "copy client ID from Google here";
                     options.ClientSecret = "copy client secret from Google here";
                 });
        }

        public static void AddCustomIdentityServer(this IServiceCollection services, string connectionString)
        {
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;

                options.UserInteraction.LoginReturnUrlParameter = "myParamName";
                options.UserInteraction.LoginUrl = "/home/login";
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));

                options.EnableTokenCleanup = true;
                options.RemoveConsumedTokens = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddResourceOwnerValidator<UserValidator>()
            .AddProfileService<ProfileService>();
        }

        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddDefaultCorsPolicy(this IServiceCollection services)
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
