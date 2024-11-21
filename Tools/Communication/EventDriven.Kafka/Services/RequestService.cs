using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace EventDriven.Kafka.Services
{
    public class RequestService<TRequest, TReply> : IRequestService<TRequest, TReply>
        where TRequest : BaseMessage
        where TReply : BaseMessage
    {
        private readonly ILogger<RequestService<TRequest, TReply>> _logger;
        private readonly IProducerService<TRequest> _producerService;
        private readonly IConsumerService<TReply> _consumerService;
        private readonly string _replyTopic;
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<TReply>> _pendingRequests = new();

        public RequestService(
            ILogger<RequestService<TRequest, TReply>> logger,
            IProducerService<TRequest> producerService,
            IConsumerService<TReply> consumerService,
            string replyTopic)
        {
            _logger = logger;
            _producerService = producerService;
            _consumerService = consumerService;
            _replyTopic = replyTopic;

            _ = Task.Run(() => StartConsumerLoop());
        }

        public async Task<TReply> SendRequestAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            var correlationId = request.CorrelationId;

            var tcs = new TaskCompletionSource<TReply>(TaskCreationOptions.RunContinuationsAsynchronously);
            if (!_pendingRequests.TryAdd(correlationId, tcs))
            {
                throw new InvalidOperationException($"Request with CorrelationId {correlationId} is already pending.");
            }

            try
            {
                await _producerService.ProduceAsync(request, cancellationToken);

                using (cancellationToken.Register(() => tcs.TrySetCanceled()))
                {
                    return await tcs.Task.WaitAsync(cancellationToken);
                }
            }
            finally
            {
                _pendingRequests.TryRemove(correlationId, out _);
            }
        }

        private async Task StartConsumerLoop()
        {
            await _consumerService.ConsumeAsync(async reply =>
            {
                await Task.Delay(1);
                if (_pendingRequests.TryRemove(reply.CorrelationId, out var tcs))
                {
                    tcs.TrySetResult(reply);
                }
            }, CancellationToken.None);
        }
    }
}
