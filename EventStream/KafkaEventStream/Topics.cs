using Confluent.Kafka.Admin;
using Confluent.Kafka;

namespace KafkaEventStream
{
    public static class Topics
    {
        public class AggregatorTopics : ITopicHandler
        {
            public string RequestTopic { get; } = "aggregator-requests";
            public string ResponseTopic { get; } = "aggregator-responses";
            public async Task CreateTopics()
            {
                await CreateTopicAsync("kafka1:9092", RequestTopic, 1);
                await CreateTopicAsync("kafka1:9092", ResponseTopic, 1);
            }
        }

        public class ContributorTopics : ITopicHandler
        {
            public string RequestTopic { get; } = "contributor-requests";
            public string ResponseTopic { get; } = "contributor-responses";

            public async Task CreateTopics()
            {
                await CreateTopicAsync("kafka1:9092", RequestTopic, 1);
                await CreateTopicAsync("kafka1:9092", ResponseTopic, 1);
            }
        }

        private static async Task CreateTopicAsync(string bootstrapServers, string topicName, int numPartitions)
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = topicName, ReplicationFactor = 1, NumPartitions = numPartitions } });
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occurred creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }
        }

        public interface ITopicHandler
        {
            string RequestTopic { get; }
            string ResponseTopic { get; }
            Task CreateTopics();
        }
    }
}
