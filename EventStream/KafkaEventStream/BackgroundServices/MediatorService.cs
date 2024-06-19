using Microsoft.Extensions.Hosting;
using EventStream.EventHandling;
using EventStream.Topics;

namespace KafkaEventStream.BackgroundServices
{
    public class MediatorService : BackgroundService
    {
        private readonly IServiceTopics _topics;
        private readonly IEventEndpointManager _eventEndpointManager;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IMessageProducer _messageProducer;

        public MediatorService(
            IServiceTopics topics, 
            IEventEndpointManager eventEndpointManager, 
            IMessageConsumer messageConsumer, 
            IMessageProducer messageProducer)
        {
            _topics = topics;
            _eventEndpointManager = eventEndpointManager;
            _messageConsumer = messageConsumer;
            _messageProducer = messageProducer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageConsumer.ConsumeAsync(_topics.RequestTopic, async message =>
            {
                var response = await _eventEndpointManager.InvokeByEndpoint(message);
                await _messageProducer.ProduceAsync(_topics.ResponseTopic, response);
            }, stoppingToken);
        }
    }
}
