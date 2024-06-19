namespace EventStream.EventHandling
{
    public interface IMessageProducer
    {
        Task ProduceAsync(string topic, string message);
    }
}
