using EventDriven.Shared.Events;
using Google.Apis.Drive.v3.Data;

namespace EventDriven.Shared.Services
{
    public interface ISagaService<TFirstEvent, TNextEvent>
        where TFirstEvent : BaseEvent
        where TNextEvent : BaseEvent
    {
        Task HandleSagaAsync(Func<TFirstEvent, Task<TNextEvent>> handler, CancellationToken cancellationToken = default);
    }
}
