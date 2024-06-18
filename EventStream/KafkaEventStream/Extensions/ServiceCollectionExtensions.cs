using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Topics;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaEventStream.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// The method creates a background service that will give requests to the mediator. 1/2
        /// </summary>
        /// <param name="services">DI service collection</param>
        /// <param name="requestFrom">A generic class that contains the names of the topics. 
        ///                           Only the name of the topic with requests for
        ///                           a mediator is required.</param>
        public static void StartRequestsBackgroundWorker(this IServiceCollection services, IServiceTopics requestFrom)
        {
            requestFrom?.CreateTopics();

            services.AddHostedService<RequestService>(provider =>
            {
                return new RequestService(requestFrom);
            });
        }

        /// <summary>
        /// The method creates a background service that will receive responses from the mediator. 2/2
        /// </summary>
        /// <typeparam name="T">Endpoint manager type.</typeparam>
        /// <param name="services">DI service collection</param>
        /// <param name="responseTo">A generic class that contains the names of the topics. 
        ///                          Only the name of the topic with responses for
        ///                          a mediator is required.</param>
        public static void StartMediatorBackgroundWorker<T>(this IServiceCollection services, IServiceTopics responseTo) where T : class, IEventEndpointManager
        {
            services.AddScoped<T>(provider =>
            {
                return ActivatorUtilities.CreateInstance<T>(provider);
            });

            services.AddHostedService<MediatorService>(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

                var scope = scopeFactory.CreateScope();
                var scopedProvider = scope.ServiceProvider;
                var serviceInvokeHandler = scopedProvider.GetService<T>();

                return new MediatorService(responseTo, serviceInvokeHandler);
            });
        }
    }
}
