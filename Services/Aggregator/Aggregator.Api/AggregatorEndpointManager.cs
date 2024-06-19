using KafkaEventStream;
using System.Text.Json;

namespace Aggregator.Api
{
    public class AggregatorEndpointManager : IEventEndpointManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentTypeService _componentTypeService;

        public AggregatorEndpointManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _componentTypeService = _serviceProvider.GetService<IComponentTypeService>();
        }

        public async Task<string> InvokeByEndpoint(string requestDest)
        {
            if (requestDest == "component/types")
            {
                var result = await _componentTypeService.GetComponentTypesAsync();
                return JsonSerializer.Serialize(result);
            }

            return string.Empty;
        }
    }
}
