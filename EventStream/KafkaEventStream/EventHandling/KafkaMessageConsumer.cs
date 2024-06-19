using Confluent.Kafka;
using EventStream.EventHandling;

namespace KafkaEventStream.EventHandling
{
    public class KafkaMessageConsumer : IMessageConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public KafkaMessageConsumer(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }

        public async Task ConsumeAsync(string topic, Action<string> messageHandler, CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            consumer.Subscribe(topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerData = consumer.Consume(TimeSpan.FromMilliseconds(1));
                if (consumerData != null)
                {
                    messageHandler.Invoke(consumerData.Message.Value);
                }
                await Task.Delay(1, stoppingToken);
            }
        }
    }
}
