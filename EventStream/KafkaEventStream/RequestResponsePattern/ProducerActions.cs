using Confluent.Kafka;
using System.Text.Json;
using static KafkaEventStream.Topics;

namespace KafkaEventStream.RequestResponsePattern
{
    public class ProducerActions
    {
        public async Task ProduceRequest(string key, ITopicHandler topicHandler, EventRequest eventRequest)
        {
            await ProduceMessage(key, topicHandler.RequestTopic, eventRequest);
        }


        public async Task ProduceResponse(string key, ITopicHandler topicHandler, EventRequest? eventRequest)
        {

            await ProduceMessage(key, topicHandler.ResponseTopic, eventRequest);
        }

        private async Task ProduceMessage(string key, string topic, EventRequest? eventRequest)
        {
            var message = new Message<string, string>()
            {
                Key = key,
                Value = JsonSerializer.Serialize(eventRequest),
            };

            // Client
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "kafka1:9092",
                Acks = Acks.All
            };

            var producer = new ProducerBuilder<string, string>(producerConfig)
                .Build();

            await producer.ProduceAsync(topic, message);

            producer.Dispose();
        }
    }
}
