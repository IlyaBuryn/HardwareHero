using KafkaEventStream.RequestResponsePattern;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using static KafkaEventStream.Topics;

namespace KafkaEventStream.Services
{
    public class RequestService : BackgroundService
    {
        private readonly ITopicHandler _topicHandler;

        public RequestService(ITopicHandler topicHandler)
        {
            _topicHandler = topicHandler;
        }

        private static string? _responseValue = null;

        public static async Task<SType> FullRequest<SType>(
            ITopicHandler topicHandler, string requestDest, CancellationToken stoppingToken = default)
        {
            await CreateRequest(Guid.NewGuid().ToString(), topicHandler, requestDest, topicHandler.RequestTopic.Split('-')[0], stoppingToken);
            var response = await GetResponse<SType>();

            return response;
        }

        public static async Task CreateRequest(
            string key, ITopicHandler topicHandler, string requestDest, string groupId, CancellationToken stoppingToken = default)
        {
            _responseValue = null;

            var producerActions = new ProducerActions();

            await producerActions.ProduceRequest(key, topicHandler, new EventRequest
            {
                Value = null,
                RequestDest = requestDest
            });
        }

        public static async Task<SType> GetResponse<SType>()
        {
            int k = 0;

            while (k <= 1000)
            {
                if (_responseValue == null)
                {
                    k++;
                    await Task.Delay(10);
                }
                else
                {
                    return JsonSerializer.Deserialize<SType>(_responseValue);
                }
            }

            throw new TimeoutException("Time out!");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerActions = new ConsumerActions();
            await consumerActions.ConsumeResponse(_topicHandler, SetResponseValue, stoppingToken);
        }

        internal void SetResponseValue(string responseValue) => _responseValue = responseValue;
    }
}
