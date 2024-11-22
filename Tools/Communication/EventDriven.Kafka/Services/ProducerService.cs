using Confluent.Kafka;
using EventDriven.Kafka.Config;
using EventDriven.Kafka.IO;
using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using Microsoft.Extensions.Logging;

namespace EventDriven.Kafka.Services
{
    public class ProducerService<TEvent> : IProducerService<TEvent> where TEvent : BaseEvent
    {
        private readonly ILogger<ProducerService<TEvent>> _logger;
        private readonly KafkaConfig _config;
        private readonly string _topic;

        public ProducerService(
            ILogger<ProducerService<TEvent>> logger,
            KafkaConfig config,
            string topic)
        {
            _logger = logger;
            _config = config;
            _topic = topic;
        }

        public async Task ProduceAsync(TEvent eventMessage, CancellationToken cancellationToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _config.BootstrapServers,
                Acks = Acks.All,
            };

            using var producer = new ProducerBuilder<Null, TEvent>(config)
                .SetValueSerializer(new EventSerializer<TEvent>())
                .Build();

            try
            {
                var message = new Message<Null, TEvent> { Value = eventMessage };
                var deliveryResult = await producer.ProduceAsync(_topic, message, cancellationToken);

                _logger.LogInformation($"Delivered event to topic '{_topic}' at offset {deliveryResult.TopicPartitionOffset}");

            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Failed to deliver event to topic '{_topic}': {e.Error.Reason}");
            }

            producer.Flush(cancellationToken);
        }
    }
}
