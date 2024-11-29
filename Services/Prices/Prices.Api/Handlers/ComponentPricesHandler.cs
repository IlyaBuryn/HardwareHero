using EventDriven.Shared.Services;
using Prices.DTOs.Events;

namespace Prices.Api.Handlers
{
    public class ComponentPricesHandler : BackgroundService
    {
        private readonly IReplyService<ComponentPriceEvent, LatestLowestComponentPriceEvent> _latestPricesService;
        private readonly IServiceProvider _serviceProvider;

        public ComponentPricesHandler(
            IReplyService<ComponentPriceEvent, LatestLowestComponentPriceEvent> latestPricesService, IServiceProvider serviceProvider)
        {
            _latestPricesService = latestPricesService;
            _serviceProvider = serviceProvider;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _latestPricesService.HandleRequestAsync(async priceEvent =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IContributorPricesService>();
                    var result = await service.GetLowestFromLatestPricesAsync(priceEvent.ComponentId);

                    return result;
                }
            }, stoppingToken);
        }
    }
}
