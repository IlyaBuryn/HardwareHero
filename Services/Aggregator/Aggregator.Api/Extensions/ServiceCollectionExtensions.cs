using Confluent.Kafka;
using EventStream.EventHandling;
using EventStream.Topics;
using FluentValidation.AspNetCore;
using KafkaEventStream.EventHandling;
using KafkaEventStream;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using KafkaEventStream.Extensions;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using HardwareHero.Shared.Extensions;

namespace Aggregator.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerConstants.AuthenticationScheme)
                .AddJwtBearer(IdentityServerConstants.AuthenticationScheme, options =>
                {
                    options.Authority = IdentityServerConstants.IdentityServerAuthority;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }

        public static void AddApiScopeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentityClientConstants.ServicesApiScope);
                });
            });
        }

        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        }

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

        public static void StartKafkaMediator<T, E>(this IServiceCollection services) 
            where T : class, IServiceTopics
            where E : class, IEventEndpointManager
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = EventStreamConstants.BootstrapServers,
                Acks = Acks.All
            };
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = EventStreamConstants.BootstrapServers,
                GroupId = EventStreamConstants.MSCommunicationGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            services.AddSingleton(producerConfig);
            services.AddSingleton(consumerConfig);

            services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
            services.AddSingleton<IMessageConsumer, KafkaMessageConsumer>();
            services.AddSingleton<IServiceTopics, T>();
            services.AddScoped<IEventEndpointManager, E>();

            services.StartMediatorBackgroundWorker<E>(
                services.BuildServiceProvider().GetRequiredService<IServiceTopics>(),
                services.BuildServiceProvider().GetRequiredService<IMessageProducer>(),
                services.BuildServiceProvider().GetRequiredService<IMessageConsumer>()
            );
        }
    }
}
