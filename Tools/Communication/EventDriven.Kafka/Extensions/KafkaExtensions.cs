using Confluent.Kafka.Admin;
using Confluent.Kafka;
using EventDriven.Kafka.Config;
using EventDriven.Kafka.Services;
using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventDriven.Kafka.Extensions
{
    public static class KafkaExtensions
    {
        public static IServiceCollection AddKafkaProducer<TEvent>(
            this IServiceCollection services,
            KafkaConfig? config,
            string? topic = null)
            where TEvent : BaseEvent
        {
            ArgumentNullException.ThrowIfNull(config, nameof(KafkaConfig));

            if (topic == null)
            {
                topic = CreateTopicName<TEvent>("Event");
            }

            services.AddSingleton(config);
            services.AddSingleton<IProducerService<TEvent>>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<ProducerService<TEvent>>>();

                return new ProducerService<TEvent>(logger, config, topic);
            });

            return services;
        }

        public static IServiceCollection AddKafkaConsumer<TEvent>(
            this IServiceCollection services,
            KafkaConfig? config,
            string? topic = null)
            where TEvent : BaseEvent
        {
            ArgumentNullException.ThrowIfNull(config, nameof(KafkaConfig));

            if (topic == null)
            {
                topic = CreateTopicName<TEvent>("Event");
            }

            CreateTopicIfNotExists(config, topic);

            services.AddSingleton(config);
            services.AddTransient<IConsumerService<TEvent>>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<ConsumerService<TEvent>>>();

                return new ConsumerService<TEvent>(logger, config, topic);
            });

            return services;
        }

        public static IServiceCollection AddKafkaRequestService<TRequest, TReply>(
            this IServiceCollection services,
            KafkaConfig? config)
            where TRequest : BaseMessage
            where TReply : BaseMessage
        {
            var requestTopic = CreateTopicName<TRequest>("Request");
            var replyTopic = CreateTopicName<TRequest>("Reply");

            services.AddKafkaProducer<TRequest>(config, requestTopic);
            services.AddKafkaConsumer<TReply>(config, replyTopic);

            CreateTopicIfNotExists(config!, requestTopic);
            CreateTopicIfNotExists(config!, replyTopic);

            services.AddSingleton<IRequestService<TRequest, TReply>>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<RequestService<TRequest, TReply>>>();
                var producer = provider.GetRequiredService<IProducerService<TRequest>>();
                var consumer = provider.GetRequiredService<IConsumerService<TReply>>();

                return new RequestService<TRequest, TReply>(logger, producer, consumer, replyTopic);
            });

            return services;
        }

        public static IServiceCollection AddKafkaReplyService<TRequest, TReply>(
            this IServiceCollection services,
            KafkaConfig? config)
            where TRequest : BaseMessage
            where TReply : BaseMessage
        {
            var requestTopic = CreateTopicName<TRequest>("Request");
            var replyTopic = CreateTopicName<TRequest>("Reply");

            services.AddKafkaProducer<TReply>(config, replyTopic);
            services.AddKafkaConsumer<TRequest>(config, requestTopic);

            services.AddSingleton<IReplyService<TRequest, TReply>>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<ReplyService<TRequest, TReply>>>();
                var producer = provider.GetRequiredService<IProducerService<TReply>>();
                var consumer = provider.GetRequiredService<IConsumerService<TRequest>>();

                return new ReplyService<TRequest, TReply>(logger, consumer, producer);
            });

            return services;
        }

        private static string CreateTopicName<TEvent>(string detail) =>
            $"{typeof(TEvent).Name}.{detail}";

        private static void CreateTopicIfNotExists(KafkaConfig config, string topic)
        {
            var adminConfig = new AdminClientConfig
            {
                BootstrapServers = config.BootstrapServers
            };

            using var adminClient = new AdminClientBuilder(adminConfig).Build();

            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
                if (metadata.Topics.Any(t => t.Topic == topic))
                {
                    Console.WriteLine($"Topic '{topic}' already exists.");
                    return;
                }

                adminClient.CreateTopicsAsync(new[]
                {
                    new TopicSpecification
                    {
                        Name = topic,
                        NumPartitions = 1,
                        ReplicationFactor = 1
                    }
                }).Wait();

                Console.WriteLine($"Topic '{topic}' created successfully.");
            }
            catch (CreateTopicsException ex)
            {
                if (ex.Results.Any(r => r.Error.Code != ErrorCode.TopicAlreadyExists))
                {
                    throw;
                }
                Console.WriteLine($"Topic '{topic}' already exists.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create topic '{topic}': {ex.Message}");
            }
        }
    }
}
