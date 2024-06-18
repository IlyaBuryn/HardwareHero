using Confluent.Kafka;
using HardwareHero.Shared.Constants;
using KafkaEventStream.Topics;

namespace KafkaEventStream.EventHandling
{
    /// <summary>
    /// A class that contains methods for receiving requests and responses.
    /// </summary>
    public class ConsumerActions
    {
        /// <summary>
        /// The method receives the message, processes it based on the endpoint, and sends a response. (Mediator).
        /// </summary>
        /// <param name="topics">Request ans response topics</param>
        /// <param name="eventEndpointManager">Endpoint manager</param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns>Task</returns>
        public async Task MediateRequestAndResponseAsync(
            IServiceTopics topics, IEventEndpointManager eventEndpointManager, CancellationToken stoppingToken)
        {
            var producerActions = new ProducerActions();

            var consumerConfig = CreateConsumerConfig();

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                .Build())
            {
                consumer.Subscribe(topics.RequestTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumerData = consumer.Consume(TimeSpan.FromMilliseconds(1));
                    if (consumerData != null)
                    {
                        var destination = consumerData.Message.Value;
                        if (!string.IsNullOrEmpty(destination)) 
                        {
                            var value = await eventEndpointManager.InvokeByEndpoint(destination);
                            await producerActions.ProduceResponseAsync(topics, value);
                        }
                    }

                    await Task.Delay(1, stoppingToken);
                }
            }
        }

        /// <summary>
        /// Receiving a response from the mediator.
        /// </summary>
        /// <param name="topics">Request ans response topics</param>
        /// <param name="receiverDelegate">The method that is called to set the response.</param>
        /// <param name="stoppingToken">Cancellation token</param>
        /// <returns></returns>
        public async Task ConsumeResponseAsync(
            IServiceTopics topics, Action<string> receiverDelegate, CancellationToken stoppingToken)
        {
            var consumerConfig = CreateConsumerConfig();

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                .Build())
            {
                consumer.Subscribe(topics.ResponseTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumerData = consumer.Consume(TimeSpan.FromMilliseconds(1));
                    if (consumerData != null)
                    {
                        receiverDelegate.Invoke(consumerData.Message.Value);
                    }

                    await Task.Delay(1, stoppingToken);
                }
            }
        }

        /// <summary>
        /// Creating a config for Kafka.
        /// </summary>
        /// <returns>ConsumerConfig</returns>
        private ConsumerConfig CreateConsumerConfig() => new ConsumerConfig()
        {
            BootstrapServers = EventStreamConstants.BootstrapServers,
            ClientId = Guid.NewGuid().ToString(),
            GroupId = EventStreamConstants.MSCommunicationGroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}
