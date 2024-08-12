using EventStream.Topics;
using KafkaEventStream.Topics;

public class MailTopics : IServiceTopics
{
    public string RequestTopic { get; } = "mail-requests";
    public string ResponseTopic { get; } = "mail-responses";

    public async Task CreateTopics()
    {
        await TopicsTools.CreateTopicAsync(RequestTopic, 2);
        await TopicsTools.CreateTopicAsync(ResponseTopic, 2);
    }
}