using Confluent.Kafka;
using HardwareHero.Shared.Constants;
using KafkaEventStream.Topics;

namespace KafkaEventStream.EventHandling
{
    /// <summary>
    /// A class that contains methods for sending requests and responses.
    /// </summary>
    public class ProducerActions
    {
        /// <summary>
        /// Send a request.
        /// </summary>
        /// <param name="topics">Request topic</param>
        /// <param name="endpoint">Endpoint</param>
        /// <returns>Task</returns>
        public async Task ProduceRequestAsync(IServiceTopics topics, string endpoint)
        {
            await ProduceMessageAsync(topics.RequestTopic, endpoint);
        }

        /// <summary>
        /// Send a response
        /// </summary>
        /// <param name="topics">Response topic</param>
        /// <param name="value">Value to send</param>
        /// <returns>Task</returns>
        public async Task ProduceResponseAsync(IServiceTopics topics, string value)
        {
            await ProduceMessageAsync(topics.ResponseTopic, value);
        }

        /// <summary>
        /// Sending a message using Kafka.
        /// </summary>
        /// <param name="topic">Response and request topics</param>
        /// <param name="value">Value to send</param>
        /// <returns>Task</returns>
        private async Task ProduceMessageAsync(string topic, string value)
        {
            var message = new Message<string, string>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = value,
            };

            // Client
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = EventStreamConstants.BootstrapServers,
                Acks = Acks.All
            };

            var producer = new ProducerBuilder<string, string>(producerConfig)
                .Build();

            await producer.ProduceAsync(topic, message);

            producer.Dispose();
        }
    }
}
