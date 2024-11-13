using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using HardwareHero.Shared.Repositories.Contracts;
using Mail.BusinessLogic.Models;
using Mail.DTOs.Mail;
using HardwareHero.Shared.Extensions.Repository;

namespace Mail.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepositoryAsync<MailMessage> _messageRepo;
        private readonly IMapper _mapper;


        public MailService(
            IConfiguration configuration,
            IBaseRepositoryAsync<MailMessage> messageRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _messageRepo = messageRepository;
            _mapper = mapper;
        }

        public async Task<Guid> SendMailAsync(MailMessageDto messageToSend)
        {
            var config = _configuration.GetSection("SMTP");
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(config.GetSection("SenderAddress").Value));
            email.To.Add(MailboxAddress.Parse(messageToSend.RecipientMailAddress));
            email.Subject = messageToSend.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = messageToSend.Body
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(config.GetSection("SenderAddress").Value, config.GetSection("SenderAddressPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            messageToSend.Id = Guid.NewGuid();
            messageToSend.Timestamp = DateTime.Now;

            var message = _mapper.Map<MailMessage>(messageToSend);
            message.Body = string.Empty;
            var result = await _messageRepo.CreateEntityAsync(message);
            result.DataAnswerCheck();

            return messageToSend.Id;
        }
    }
}
