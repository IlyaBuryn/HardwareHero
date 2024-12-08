using EventDriven.Shared.Events;
using EventDriven.Shared.Services;
using HardwareHero.Shared.Exceptions;

namespace References.Api.Handlers
{
    public class ReferencesEventsHandler : BackgroundService
    {
        private readonly IReplyService<CurrencyRequestMessage, CurrencyDto> _currencyService;
        private readonly IReplyService<RegionRequestMessage, RegionDto> _regionService;
        private readonly IServiceProvider _serviceProvider;

        public ReferencesEventsHandler(
            IReplyService<CurrencyRequestMessage, CurrencyDto> currencyService,
            IReplyService<RegionRequestMessage, RegionDto> regionService,
            IServiceProvider serviceProvider)
        {
            _currencyService = currencyService;
            _regionService = regionService;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new List<Task>
            {
                ProcessRequestsAsync(_currencyService, async currencyEvent =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<ICurrencyService>();
                    var result = await service.FindCurrencyAsync(currencyEvent);
                    if (result == null)
                    {
                        result = new CurrencyDto(new NotFoundException(nameof(Currency)));
                    }

                    return result;
                }, stoppingToken),

                ProcessRequestsAsync(_regionService, async regionEvent =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IRegionService>();
                    var result = await service.FindRegionAsync(regionEvent);
                    if (result == null)
                    {
                        result = new RegionDto(new NotFoundException(nameof(Region)));
                    }

                    return result;
                }, stoppingToken),
            };

            await Task.WhenAll(tasks);
        }

        private async Task ProcessRequestsAsync<TRequest, TReply>(
            IReplyService<TRequest, TReply> replyService,
            Func<TRequest, Task<TReply>> handler,
            CancellationToken cancellationToken)
            where TRequest : BaseMessage
            where TReply : BaseMessage
        {
            await replyService.HandleRequestAsync(handler, cancellationToken);
        }
    }
}
