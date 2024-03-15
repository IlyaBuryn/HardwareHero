namespace KafkaEventStream
{
    public interface IServiceInvokeHandler
    {
        Task<string> InvokeMethodFromMessage(string requestDest);
    }
}
