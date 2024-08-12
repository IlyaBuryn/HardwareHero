using KafkaEventStream.Contracts;
using System.Text.Json;

namespace Aggregator.Api
{
    public class AggregatorEndpointManager : EventEndpointManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentTypeService _componentTypeService;

        public AggregatorEndpointManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _componentTypeService = _serviceProvider.GetService<IComponentTypeService>();
        }

        public override async Task<string> InvokeByEndpoint(string endpoint)
        {
            if (IsMatchEndpoints(endpoint, "component/types"))
            {
                var result = await _componentTypeService.GetComponentTypesAsync();
                return JsonSerializer.Serialize(result);
            }

            return string.Empty;
        }
    }
}
