using Confluent.Kafka;
using System.Text.Json;
using static KafkaEventStream.Topics;

namespace KafkaEventStream.RequestResponsePattern
{
    public class ConsumerActions
    {
        public async Task ConsumeRequestAndProduceResponse(
            ITopicHandler topicHandler, IServiceInvokeHandler serviceInvokeHandler, CancellationToken stoppingToken)
        {
            var producerActions = new ProducerActions();

            var consumerConfig = CreateConsumerConfig();

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                .Build())
            {
                consumer.Subscribe(topicHandler.RequestTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumerData = consumer.Consume(TimeSpan.FromMilliseconds(5));
                    if (consumerData != null)
                    {
                        var request = JsonSerializer.Deserialize<EventRequest>(consumerData.Message.Value);
                        if (request != null)
                        {
                            request.Value = await serviceInvokeHandler.InvokeMethodFromMessage(request.RequestDest);
                            await producerActions.ProduceResponse(Guid.NewGuid().ToString(), topicHandler, request);
                        }
                    }

                    await Task.Delay(1, stoppingToken);
                }
            }
        }

        public async Task ConsumeResponse(
            ITopicHandler topicHandler, Action<string> whereToStoreResponse, CancellationToken stoppingToken)
        {
            var consumerConfig = CreateConsumerConfig();

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig)
                .Build())
            {
                consumer.Subscribe(topicHandler.ResponseTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumerData = consumer.Consume(TimeSpan.FromMilliseconds(5));
                    if (consumerData != null)
                    {
                        var responseEventRequest = JsonSerializer.Deserialize<EventRequest>(consumerData.Message.Value);
                        var responseValue = responseEventRequest?.Value ?? "Serialization error!";
                        whereToStoreResponse.Invoke(responseValue);
                    }

                    await Task.Delay(1, stoppingToken);
                }
            }
        }

        private ConsumerConfig CreateConsumerConfig() => new ConsumerConfig()
        {
            BootstrapServers = "kafka1:9092",
            ClientId = Guid.NewGuid().ToString(),
            GroupId = "HardwareHero",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}
