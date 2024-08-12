using Mail.BusinessLogic.Presets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mail.BusinessLogic.Services
{
    public class MailServicePresets : MailService, IMailServicePresets
    {
        private readonly IConfiguration _configuration;
        private Dictionary<MailPresets, Preset?> _mailTemplatePaths;

        public MailServicePresets(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper, IConfiguration configuration) 
            : base(databaseSettings, mapper, configuration)
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
