using EventDriven.Shared.Events;

namespace EventDriven.Shared.Services
{
    public interface IConsumerService<TEvent> where TEvent : BaseEvent
    {
        Task ConsumeAsync(Func<TEvent, Task> handler, CancellationToken cancellationToken);
    }
}
