using EventDriven.Shared.Events;

namespace EventDriven.Shared.Services
{
    public interface IProducerService<TEvent> where TEvent : BaseEvent
    {
        Task ProduceAsync(TEvent eventMessage, CancellationToken cancellationToken);
    }
}
