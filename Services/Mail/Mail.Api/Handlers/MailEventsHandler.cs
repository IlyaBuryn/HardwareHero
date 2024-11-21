using EventDriven.Shared.Services;
using Mail.BusinessLogic.Services;
using Mail.DTOs.Events;
using Mail.DTOs.Mail;
using System.Text.Json;

namespace Mail.Api.Handlers
{
    public class MailEventsHandler : BackgroundService
    {
        private readonly IConsumerService<MailSettingsEvent> _consumerService;
        //private readonly IMailServicePresets _mailServicePresets;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MailEventsHandler> _logger;

        public MailEventsHandler(
            IConsumerService<MailSettingsEvent> consumerService,
            ILogger<MailEventsHandler> logger,
            IServiceProvider serviceProvider)
        {
            _consumerService = consumerService;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ConsumeAsync(async mailEvent =>
            {
                _logger.LogInformation($"Processing event: {JsonSerializer.Serialize(mailEvent)}");

                var mail = new MailMessageDto()
                {
                    SenderId = Guid.NewGuid(),
                    Status = "sent",
                    Subject = "-",
                    Body = "-",
                    RecipientId = Guid.NewGuid(),
                    RecipientMailAddress = mailEvent.RecipientMailAddress,
                    Id = Guid.NewGuid(),
                    Timestamp = mailEvent.Timestamp,
                    MailSettingsEvent = mailEvent
                };

                using (var scope = _serviceProvider.CreateScope())
                {
                    var mailServicePresets = scope.ServiceProvider.GetRequiredService<IMailServicePresets>();
                    await mailServicePresets.SendMailAsync(mail, MailPresets.Welcome);
                }
            }, stoppingToken);
        }
    }
}