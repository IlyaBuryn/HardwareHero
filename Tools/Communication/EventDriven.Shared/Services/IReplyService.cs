using EventDriven.Shared.Events;

namespace EventDriven.Shared.Services
{
    public interface IReplyService<TRequest, TReply>
        where TRequest : BaseMessage
        where TReply : BaseMessage
    {
        Task HandleRequestAsync(Func<TRequest, Task<TReply>> handler, CancellationToken cancellationToken = default);
    }
}
