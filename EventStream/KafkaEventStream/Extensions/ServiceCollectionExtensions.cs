using EventStream.EventHandling;
using EventStream.Topics;
using KafkaEventStream.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaEventStream.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void StartRequestsBackgroundWorker(
            this IServiceCollection services, 
            IServiceTopics requestFrom, 
            IMessageProducer messageProducer, IMessageConsumer messageConsumer)
        {
            requestFrom?.CreateTopics();

            services.AddHostedService<RequestService>(provider => 
                new RequestService(requestFrom, messageProducer, messageConsumer));
        }

        public static void StartMediatorBackgroundWorker<T>(
            this IServiceCollection services, 
            IServiceTopics responseTo, 
            IMessageProducer messageProducer, IMessageConsumer messageConsumer) 
                where T : class, IEventEndpointManager
        {
            services.AddScoped<T>();
            services.AddHostedService<MediatorService>(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

                var scope = scopeFactory.CreateScope();
                var scopedProvider = scope.ServiceProvider;
                var serviceInvokeHandler = scopedProvider.GetService<T>();

                return new MediatorService(responseTo, serviceInvokeHandler, messageConsumer, messageProducer);
            });
        }
    }
}
