using EventDriven.Shared.Services;
using Mail.DTOs.Events;
using System.Text.Json;
using System.Threading;

namespace Mail.Api.Handlers
{
    public class MailEventsHandler : BackgroundService
    {
        private readonly IConsumerService<SendMailEvent> _consumerService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MailEventsHandler> _logger;

        public MailEventsHandler(
            IConsumerService<SendMailEvent> consumerService,
            ILogger<MailEventsHandler> logger,
            IServiceProvider serviceProvider)
        {
            _consumerService = consumerService;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine(1);

            await _consumerService.ConsumeAsync(async mailEvent =>
            {
                _logger.LogInformation($"Processing event: {JsonSerializer.Serialize(mailEvent)}");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var mailServicePresets = scope.ServiceProvider.GetRequiredService<IMailServicePresets>();
                    await mailServicePresets.SendMailTemplateAsync(mailEvent);
                }
            }, stoppingToken);

            Console.WriteLine(2);
        }
    }
}