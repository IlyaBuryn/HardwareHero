namespace KafkaEventStream
{
    public interface IEventEndpointManager
    {
        Task<string> InvokeByEndpoint(string endpoint);
    }
}
