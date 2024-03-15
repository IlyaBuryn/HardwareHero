using Microsoft.Extensions.Hosting;
using static KafkaEventStream.Topics;
using KafkaEventStream.RequestResponsePattern;

namespace KafkaEventStream.Services
{
    public class ResponseService : BackgroundService
    {
        private readonly ITopicHandler _topicHandler;
        private readonly IServiceInvokeHandler _serviceInvokeHandler;

        public ResponseService(ITopicHandler topicHandler, IServiceInvokeHandler serviceInvokeHandler)
        {
            _topicHandler = topicHandler;
            _serviceInvokeHandler = serviceInvokeHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerActions = new ConsumerActions();

            await consumerActions.ConsumeRequestAndProduceResponse(_topicHandler, _serviceInvokeHandler, stoppingToken);
        }
    }
}
