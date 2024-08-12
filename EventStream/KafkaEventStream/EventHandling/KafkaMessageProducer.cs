using Confluent.Kafka;
using EventStream.EventHandling;

namespace KafkaEventStream.EventHandling
{
    public class KafkaMessageProducer : IMessageProducer
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaMessageProducer(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }

        public async Task ProduceAsync(string topic, string message)
        {
            using var producer = new ProducerBuilder<string, string>(_producerConfig).Build();
            var kafkaMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message,
            };

            await producer.ProduceAsync(topic, kafkaMessage);
        }
    }
}
