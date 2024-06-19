using EventStream.EventHandling;
using EventStream.Topics;
using Microsoft.Extensions.Hosting;

namespace KafkaEventStream.BackgroundServices
{
    public class RequestService : BackgroundService
    {
        private readonly IServiceTopics _topics;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;

        private static string? _responseResult = null;
        private static int _ticsForResponse = 1000; // 1000 == 10 sec.

        public RequestService(
            IServiceTopics topics, 
            IMessageProducer messageProducer, 
            IMessageConsumer messageConsumer)
        {
            _topics = topics;
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
        }

        public static async Task<string> CallAndWaitServiceAsync(
            IServiceTopics topics, string endpoint, 
            IMessageProducer messageProducer, IMessageConsumer messageConsumer, 
            CancellationToken stoppingToken = default)
        {
            await CreateRequestToServiceAsync(topics, endpoint, messageProducer, stoppingToken);
            var response = await GetResponseFromServiceAsync(messageConsumer, stoppingToken);

            return response;
        }

        private static async Task CreateRequestToServiceAsync(
            IServiceTopics topics, string endpoint, 
            IMessageProducer messageProducer, 
            CancellationToken stoppingToken = default)
        {
            _responseResult = null;
            await messageProducer.ProduceAsync(topics.RequestTopic, endpoint);
        }

        private static async Task<string> GetResponseFromServiceAsync(
            IMessageConsumer messageConsumer, CancellationToken stoppingToken)
        {
            int k = 0;

            while (k <= _ticsForResponse)
            {
                if (_responseResult == null)
                {
                    k++;
                    await Task.Delay(10, stoppingToken);
                }
                else
                {
                    return _responseResult;
                }
            }

            throw new TimeoutException("Time out!");
        }

        internal void SetResponseResult(string responseResult) => _responseResult = responseResult;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageConsumer.ConsumeAsync(_topics.ResponseTopic, message =>
            {
                SetResponseResult(message);
            }, stoppingToken);
        }
    }
}
