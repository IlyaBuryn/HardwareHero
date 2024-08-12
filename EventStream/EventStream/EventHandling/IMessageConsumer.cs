namespace EventStream.EventHandling
{
    public interface IMessageConsumer
    {
        Task ConsumeAsync(string topic, Action<string> messageHandler, CancellationToken stoppingToken);
    }
}
