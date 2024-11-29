using EventDriven.Kafka.Config;
using EventDriven.Kafka.Extensions;
using FluentValidation;
using Identity.Shared.Domain;
using Identity.Shared.Events;
using Mail.DTOs.Events;
using Storage.DTOs.Events;
using Users.Api.Contracts;
using Users.Api.Data;
using Users.Api.Handlers;
using Users.Api.Services;

namespace Users.Api.Extensions
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
                "UsersRemoteManage",
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
                .ConfigureCommonSQLServerContext<UsersDbContext>
                    (builder, ConnectionNames.UsersConnection);

            return services;
        }

        public static IServiceCollection ConfigureEventServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            var messageConfig = builder.Configuration.GetSection("MessageKafkaConfig").Get<KafkaConfig>();
            var eventConfig = builder.Configuration.GetSection("EventKafkaConfig").Get<KafkaConfig>();

            services
                .AddKafkaRequestService<UploadFileEvent, ReturnFileUrlEvent>(messageConfig)
                .AddKafkaRequestService<ChangeFileEvent, ReturnFileUrlEvent>(messageConfig)
                .AddKafkaRequestService<DeleteFileEvent, DeleteFileResultEvent>(messageConfig)

                .AddKafkaReplyService<CreateUserEvent, UserResultEvent>(messageConfig)
                .AddKafkaReplyService<UpdateUserEvent, UserResultEvent>(messageConfig)
                .AddKafkaReplyService<DeleteUserEvent, UserResultEvent>(messageConfig)

                .AddKafkaConsumer<FindUserToMailSagaEvent>(eventConfig)

                .AddKafkaProducer<SendMailEvent>(eventConfig);

            return services;
        }

        public static IServiceCollection ConfigureEventHandlers(
            this IServiceCollection services)
        {
            services
                .AddHostedService<UserEventsHandler>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUsersService, UsersService>()
                .AddScoped<IRolesService, RolesService>()
                .AddScoped<IWishListService, WishListService>()

                .AddScoped<DefaultDataSeed>();

            return services;
        }

        public static IServiceCollection ConfigureCORSPolicy(this IServiceCollection services)
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

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
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

            return services;
        }

        public static IServiceCollection ConfigurePolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", HardwareHero.Shared.Constants.IdentityConstants.ServicesApiScope);
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureCustomIdentity(this IServiceCollection services)
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
    }
}
