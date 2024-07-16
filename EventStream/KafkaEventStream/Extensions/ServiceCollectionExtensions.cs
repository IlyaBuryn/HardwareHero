using Confluent.Kafka;
using EventStream.EventHandling;
using EventStream.Topics;
using HardwareHero.Shared.Constants;
using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Contracts;
using KafkaEventStream.EventHandling;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaEventStream.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void StartRequestsBackgroundWorker(
            this IServiceCollection services, 
            IServiceTopics requestFrom, 
            IMessageProducer messageProducer, IMessageConsumer messageConsumer)
        {
            requestFrom?.CreateTopics();

            services.AddHostedService<RequestService>(provider => 
                new RequestService(requestFrom, messageProducer, messageConsumer));
        }

        public static void ConfigureKafkaRequestsBackgroundWorker<TTopic>(
            this IServiceCollection services)
            where TTopic : class, IServiceTopics
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = EventStreamConstants.MailBootstrapServers,
                Acks = Acks.All
            };
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = EventStreamConstants.MailBootstrapServers,
                GroupId = EventStreamConstants.MSCommunicationGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            services.AddSingleton(producerConfig);
            services.AddSingleton(consumerConfig);

            services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
            services.AddSingleton<IMessageConsumer, KafkaMessageConsumer>();
            services.AddSingleton<IServiceTopics, TTopic>();

            services.StartRequestsBackgroundWorker(
                services.BuildServiceProvider().GetRequiredService<IServiceTopics>(),
                services.BuildServiceProvider().GetRequiredService<IMessageProducer>(),
                services.BuildServiceProvider().GetRequiredService<IMessageConsumer>()
            );
        }

        public static void StartMediatorBackgroundWorker<T>(
            this IServiceCollection services,
            IServiceTopics responseTo,
            IMessageProducer messageProducer, IMessageConsumer messageConsumer) 
                where T : EventEndpointManager
        {
            services.AddScoped<T>();
            services.AddHostedService<MediatorService>(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

                var scope = scopeFactory.CreateScope();
                var scopedProvider = scope.ServiceProvider;
                var serviceInvokeHandler = scopedProvider.GetService<T>();

                return new MediatorService(responseTo, serviceInvokeHandler, messageConsumer, messageProducer);
            });
        }

        public static void ConfigureKafkaMediatorBackgroundWorker<TTopic, TEndpoints>(
            this IServiceCollection services)
            where TTopic : class, IServiceTopics
            where TEndpoints : EventEndpointManager
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = EventStreamConstants.DataBootstrapServers,
                Acks = Acks.All
            };
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = EventStreamConstants.DataBootstrapServers,
                GroupId = EventStreamConstants.MSCommunicationGroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            services.AddSingleton(producerConfig);
            services.AddSingleton(consumerConfig);

            services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
            services.AddSingleton<IMessageConsumer, KafkaMessageConsumer>();
            services.AddSingleton<IServiceTopics, TTopic>();
            services.AddScoped<EventEndpointManager, TEndpoints>();

            services.StartMediatorBackgroundWorker<TEndpoints>(
                services.BuildServiceProvider().GetRequiredService<IServiceTopics>(),
                services.BuildServiceProvider().GetRequiredService<IMessageProducer>(),
                services.BuildServiceProvider().GetRequiredService<IMessageConsumer>()
            );
        }
    }
}
