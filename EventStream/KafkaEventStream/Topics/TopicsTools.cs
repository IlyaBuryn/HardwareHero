using Confluent.Kafka.Admin;
using Confluent.Kafka;
using HardwareHero.Shared.Constants;

namespace KafkaEventStream.Topics
{
    public static class TopicsTools
    {
        public static async Task CreateTopicAsync(string topicName, int numPartitions)
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig
            { BootstrapServers = EventStreamConstants.BootstrapServers }).Build())
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
    }
}
