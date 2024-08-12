using EventStream.Topics;

namespace KafkaEventStream.Topics
{
    public class IdentityTopics : IServiceTopics
    {
        public string RequestTopic { get; } = "identity-requests";
        public string ResponseTopic { get; } = "identity-responses";
        public async Task CreateTopics()
        {
            await TopicsTools.CreateTopicAsync(RequestTopic, 2);
            await TopicsTools.CreateTopicAsync(ResponseTopic, 2);
        }
    }
}
