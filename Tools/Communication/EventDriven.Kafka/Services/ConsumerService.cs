using Confluent.Kafka;
using EventDriven.Kafka.Config;
using EventDriven.Kafka.IO;
using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using Microsoft.Extensions.Logging;

namespace EventDriven.Kafka.Services
{
    public class ConsumerService<TEvent> : IConsumerService<TEvent> where TEvent : BaseEvent
    {
        private readonly ILogger<ConsumerService<TEvent>> _logger;
        private readonly KafkaConfig _config;
        private readonly string _topic;

        public ConsumerService(
            ILogger<ConsumerService<TEvent>> logger,
            KafkaConfig config,
            string topic)
        {
            _logger = logger;
            _config = config;
            _topic = topic;
        }

        public async Task ConsumeAsync(Func<TEvent, Task> handler, CancellationToken cancellationToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config.BootstrapServers,
                GroupId = _config.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, TEvent>(consumerConfig)
                .SetValueDeserializer(new EventDeserializer<TEvent>())
                .Build();

            consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(cancellationToken);
                    if (result?.Message?.Value != null)
                    {
                        _logger.LogInformation($"Consumed event at: {result.TopicPartitionOffset}");
                        await handler(result.Message.Value);
                    }
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error consuming event from topic '{_topic}': {e.Error.Reason}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
