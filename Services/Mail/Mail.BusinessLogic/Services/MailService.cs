using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Mail.DTOs.Events;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Extensions.Repository;

namespace Mail.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepositoryAsync<SendMailEvent> _messagesRepo;


        public MailService(
            IConfiguration configuration,
            IBaseRepositoryAsync<SendMailEvent> messagesRepo)
        {
            _configuration = configuration;
            _messagesRepo = messagesRepo;
        }


        public async Task<Guid?> SendMailAsync(SendMailEvent messageToSend)
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

            return await Task.FromResult(messageToSend.Id);
        }


        public async Task<Guid?> SaveMailAsync(SendMailEvent messageToSave)
        {
            messageToSave.Id = Guid.NewGuid();
            messageToSave.Body = string.Empty;

            var result = await _messagesRepo.CreateEntityAsync(messageToSave);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }
    }
}
