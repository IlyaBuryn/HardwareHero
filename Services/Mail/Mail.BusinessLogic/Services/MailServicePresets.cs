using HardwareHero.Shared.Repositories.Contracts;
using Mail.BusinessLogic.Models;
using Mail.BusinessLogic.Presets;
using Mail.DTOs.Mail;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Services
{
    public class MailServicePresets : MailService, IMailServicePresets
    {
        private readonly IConfiguration _configuration;
        private Dictionary<MailPresets, Preset?> _mailTemplatePaths;

        public MailServicePresets(
            IBaseRepositoryAsync<MailMessage> repository,
            IMapper mapper, IConfiguration configuration) 
            : base(configuration, repository, mapper)
        {
            _configuration = configuration;
        }

        public async Task<Guid> SendMailAsync(MailMessageDto mailMessage, MailPresets mailPresets)
        {
            GenerateTemplateDictionary();

            if (_mailTemplatePaths[mailPresets] != null)
            {
                bool customizeResult;
                (mailMessage, customizeResult) = _mailTemplatePaths[mailPresets].CustomizeMessageBody(mailMessage);

                if (!customizeResult)
                {
                    mailMessage.Status = "Can't customize";
                }
            }

            var result = await SendMailAsync(mailMessage);

            return result;
        }

        private void GenerateTemplateDictionary()
        {
            _mailTemplatePaths = new()
            {
                { MailPresets.None, null },
                { MailPresets.Welcome, new WelcomePreset(_configuration) }
            };
        }
    }
}
