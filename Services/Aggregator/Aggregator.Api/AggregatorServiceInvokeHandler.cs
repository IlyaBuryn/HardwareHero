using KafkaEventStream;
using System.Text.Json;

namespace Aggregator.Api
{
    public class AggregatorServiceInvokeHandler : IServiceInvokeHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IComponentTypeService _componentTypeService;

        public AggregatorServiceInvokeHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _componentTypeService = _serviceProvider.GetService<IComponentTypeService>();
        }

        public async Task<string> InvokeMethodFromMessage(string requestDest)
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
