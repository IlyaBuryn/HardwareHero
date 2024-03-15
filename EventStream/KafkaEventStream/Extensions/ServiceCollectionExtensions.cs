using KafkaEventStream.Services;
using Microsoft.Extensions.DependencyInjection;
using static KafkaEventStream.Topics;

namespace KafkaEventStream.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void StartRequestsWorker(this IServiceCollection services, ITopicHandler requestFrom)
        {
            requestFrom?.CreateTopics();

            services.AddHostedService<RequestService>(provider =>
            {
                return new RequestService(requestFrom);
            });
        }

        public static void StartResponseWorker<T>(this IServiceCollection services, ITopicHandler responseTo) where T : class, IServiceInvokeHandler
        {
            services.AddHostedService<ResponseService>(provider =>
            {
                var groupId = responseTo.ResponseTopic.Split('-')[0];

                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

                var scope = scopeFactory.CreateScope();
                var scopedProvider = scope.ServiceProvider;
                var serviceInvokeHandler = scopedProvider.GetService<T>();

                return new ResponseService(responseTo, serviceInvokeHandler);
            });
        }
    }
}
