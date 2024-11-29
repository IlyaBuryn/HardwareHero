using HardwareHero.Shared.Repositories.Contracts;
using Mail.BusinessLogic.Presets;
using Mail.DTOs;
using Mail.DTOs.Events;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Services
{
    public class MailServicePresets : MailService, IMailServicePresets
    {
        private readonly IConfiguration _configuration;
        private Dictionary<MailPreset, Preset?> _mailTemplatePaths;

        public MailServicePresets(
            IBaseRepositoryAsync<SendMailEvent> repository,
            IConfiguration configuration) 
            : base(configuration, repository)
        {
            _configuration = configuration;
            _mailTemplatePaths = new()
            {
                { MailPreset.None, null },
                { MailPreset.Welcome, new WelcomePreset(_configuration) }
            };
        }


        public async Task<Guid?> SendMailTemplateAsync(SendMailEvent mailEvent)
        {
            if (_mailTemplatePaths[mailEvent.MailPreset] != null)
            {
                bool customizeResult;
                (mailEvent, customizeResult) = _mailTemplatePaths[mailEvent.MailPreset]
                    .CustomizeMessageBody(mailEvent);

                if (!customizeResult)
                {
                    mailEvent.Status = "Can't customize";
                }
            }

            var sendResult = await SendMailAsync(mailEvent);
            var saveResult = await SaveMailAsync(mailEvent);

            return saveResult;
        }
    }
}
