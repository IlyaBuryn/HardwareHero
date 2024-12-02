using EventDriven.Kafka.Config;
using EventDriven.Kafka.Extensions;
using Identity.Shared.Events;
using Mail.DTOs.Events;

namespace Gateway.Ocelot.Extensions
{
    public static class ServiceCollectionExtensions
    {
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

        public static IServiceCollection ConfigureEventServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            var messageConfig = builder.Configuration.GetSection("MessageKafkaConfig").Get<KafkaConfig>();

            builder.Services
                .AddKafkaRequestService<TokenRequestEvent, AuthResultEvent>(messageConfig);

            return services;
        }
    }
}
