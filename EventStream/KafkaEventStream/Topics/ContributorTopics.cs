using EventStream.Topics;

namespace KafkaEventStream.Topics
{
    public class ContributorTopics : IServiceTopics
    {
        public string RequestTopic { get; } = "contributor-requests";
        public string ResponseTopic { get; } = "contributor-responses";

        public async Task CreateTopics()
        {
            await TopicsTools.CreateTopicAsync(RequestTopic, 4);
            await TopicsTools.CreateTopicAsync(ResponseTopic, 4);
        }
    }
}
