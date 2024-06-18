using Microsoft.Extensions.Hosting;
using KafkaEventStream.EventHandling;
using KafkaEventStream.Topics;

namespace KafkaEventStream.BackgroundServices
{
    /// <summary>
    /// 
    /// </summary>
    public class MediatorService : BackgroundService
    {
        private readonly IServiceTopics _topics;

        /// <summary>
        /// A class object that redirects endpoints and calls the required methods.
        /// </summary>
        private readonly IEventEndpointManager _eventEndpointManager;

        public MediatorService(IServiceTopics topics, IEventEndpointManager serviceInvokeHandler)
        {
            _topics = topics;
            _eventEndpointManager = serviceInvokeHandler;
        }

        /// <summary>
        /// Implementation of the default abstract method ExecuteAsync.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerActions = new ConsumerActions();

            await consumerActions.MediateRequestAndResponseAsync(_topics, _eventEndpointManager, stoppingToken);
        }
    }
}
