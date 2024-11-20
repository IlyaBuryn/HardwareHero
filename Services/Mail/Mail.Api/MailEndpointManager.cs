using KafkaEventStream.Contracts;
using Mail.BusinessLogic.Services;
using Mail.DTOs.Mail;

namespace Mail.Api
{
    public class MailEndpointManager : EventEndpointManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMailServicePresets _mailServicePresets;

        public MailEndpointManager(IServiceProvider serviceProvider, IMailServicePresets mailServicePresets)
        {
            _serviceProvider = serviceProvider; 
            _mailServicePresets = mailServicePresets;
        }

        public override async Task<string> InvokeByEndpoint(string endpoint)
        {
            if (IsMatchEndpointsAndPopulate(endpoint, "mail/welcome/{RecipientMailAddress}", out WelcomeMailData data))
            {
                var mail = new MailMessageDto()
                {
                    SenderId = Guid.NewGuid(),
                    Status = endpoint,
                    Subject = "-",
                    Body = "-",
                    RecipientId = Guid.NewGuid(),
                    RecipientMailAddress = data.RecipientMailAddress,
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                };

                var result = await _mailServicePresets.SendMailAsync(mail, MailPresets.Welcome);
                return result.ToString();
            }
            else if (endpoint == "mail/1")
            {
                var mail = new MailMessageDto()
                {
                    SenderId = Guid.NewGuid(),
                    Status = endpoint,
                    Subject = "-",
                    Body = "-",
                    RecipientId = Guid.NewGuid(),
                    RecipientMailAddress = "issaac.bishop@gmail.com",
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                };

                var result = await _mailServicePresets.SendMailAsync(mail, MailPresets.Welcome);
                return result.ToString();
            }

            return string.Empty;
        }

        private class WelcomeMailData
        {
            public string RecipientMailAddress { get; set; }
        }
    }
}
