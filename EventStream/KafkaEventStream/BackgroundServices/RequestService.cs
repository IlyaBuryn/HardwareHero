using KafkaEventStream.EventHandling;
using KafkaEventStream.Topics;
using Microsoft.Extensions.Hosting;

namespace KafkaEventStream.BackgroundServices
{
    /// <summary>
    /// A specific background service sends a request and waits for a response.
    /// </summary>
    public class RequestService : BackgroundService
    {
        private readonly IServiceTopics _topics;

        public RequestService(IServiceTopics topics)
        {
            _topics = topics;
        }

        /// <summary>
        /// The value that should change if a response is received.
        /// </summary>
        private static string? _responseResult = null;

        /// <summary>
        /// The time allocated to receive a response.
        /// </summary>
        private static int _ticsForResponse = 1000; // 1000 == 10 sec.

        /// <summary>
        /// The method sends a request and waits for a response.
        /// </summary>
        /// <param name="topics">Object that contains the names of the topics.</param>
        /// <param name="endpoint">Endpoint for which the result should be retrieved.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        /// <returns>Received response.</returns>
        public static async Task<string> CallAndWaitServiceAsync(
            IServiceTopics topics, string endpoint, CancellationToken stoppingToken = default)
        {
            await CreateRequestToServiceAsync(topics, endpoint, stoppingToken);
            var response = await GetResponseFromServiceAsync();

            return response;
        }

        /// <summary>
        /// The method that sends the request.
        /// </summary>
        /// <param name="topics">Object that contains the names of the topics.</param>
        /// <param name="endpoint">Endpoint for which the result should be retrieved.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        /// <returns>Task</returns>
        private static async Task CreateRequestToServiceAsync(
            IServiceTopics topics, string endpoint, CancellationToken stoppingToken = default)
        {
            _responseResult = null;

            var producerActions = new ProducerActions();

            await producerActions.ProduceRequestAsync(topics, endpoint);
        }

        /// <summary>
        /// Checking the value of the response over time.
        /// </summary>
        /// <returns>Received response.</returns>
        /// <exception cref="TimeoutException">If the call is called the call time will expire.</exception>
        private static async Task<string> GetResponseFromServiceAsync()
        {
            int k = 0;

            while (k <= _ticsForResponse)
            {
                if (_responseResult == null)
                {
                    k++;
                    await Task.Delay(10);
                }
                else
                {
                    return _responseResult;
                }
            }

            throw new TimeoutException("Time out!");
        }

        /// <summary>
        /// A method that is used externally by other services to set the response.
        /// </summary>
        /// <param name="responseResult">Response</param>
        internal void SetResponseResult(string responseResult) => _responseResult = responseResult;

        /// <summary>
        /// Implementation of the default abstract method ExecuteAsync.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerActions = new ConsumerActions();
            await consumerActions.ConsumeResponseAsync(_topics, SetResponseResult, stoppingToken);
        }
    }
}
