namespace EventStream.Topics
{
    public interface IServiceTopics
    {
        string RequestTopic { get; }
        string ResponseTopic { get; }
        Task CreateTopics();
    }
}
