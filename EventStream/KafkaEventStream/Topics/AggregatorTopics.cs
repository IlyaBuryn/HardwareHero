using EventStream.Topics;

namespace KafkaEventStream.Topics
{
    public class AggregatorTopics : IServiceTopics
    {
        public string RequestTopic { get; } = "aggregator-requests";
        public string ResponseTopic { get; } = "aggregator-responses";
        public async Task CreateTopics()
        {
            await TopicsTools.CreateTopicAsync(RequestTopic, 2);
            await TopicsTools.CreateTopicAsync(ResponseTopic, 2);
        }
    }
}
