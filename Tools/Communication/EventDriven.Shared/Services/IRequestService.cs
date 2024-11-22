using EventDriven.Shared.Events;

namespace EventDriven.Shared.Services
{
    public interface IRequestService<TRequest, TReply> 
        where TRequest : BaseMessage
        where TReply : BaseMessage
    {
        Task<TReply> SendRequestAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
