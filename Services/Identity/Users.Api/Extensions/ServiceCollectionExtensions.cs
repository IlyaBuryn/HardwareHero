using Users.Api.Contracts;
using Users.Api.Services;

namespace Users.Api.Extensions
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
                "UsersRemoteManage",
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
            services.ConfigureCommonSQLServerContext<UsersDbContext>(builder, ConnectionNames.UsersConnection);
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IWishListService, WishListService>();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users.Api", Version = "v1" });
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

        public static void ConfigurePolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    //policy.RequireAuthenticatedUser();
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
    }
}
