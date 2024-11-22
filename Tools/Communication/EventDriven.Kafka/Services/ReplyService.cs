using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using Microsoft.Extensions.Logging;

namespace EventDriven.Kafka.Services
{
    public class ReplyService<TRequest, TReply> : IReplyService<TRequest, TReply>
        where TRequest : BaseMessage
        where TReply : BaseMessage
    {
        private readonly ILogger<ReplyService<TRequest, TReply>> _logger;
        private readonly IConsumerService<TRequest> _consumerService;
        private readonly IProducerService<TReply> _producerService;

        public ReplyService(
            ILogger<ReplyService<TRequest, TReply>> logger,
            IConsumerService<TRequest> consumerService,
            IProducerService<TReply> producerService)
        {
            _logger = logger;
            _consumerService = consumerService;
            _producerService = producerService;
        }

        public async Task HandleRequestAsync(Func<TRequest, Task<TReply>> handler, CancellationToken cancellationToken = default)
        {
            await _consumerService.ConsumeAsync(async req =>
            {
                try
                {
                    var response = await handler(req);
                    response.CorrelationId = req.CorrelationId;

                    await _producerService.ProduceAsync(response, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing request: {ex.Message}");
                }
            }, cancellationToken);
        }
    }
}
