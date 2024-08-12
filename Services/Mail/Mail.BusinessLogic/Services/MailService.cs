using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;

namespace Mail.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<HardwareHero.Shared.Models.Mail.MailMessage> _mailCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;

        public MailService(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper,
            IConfiguration configuration)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _mailCollection = mongoDb
                .GetCollection<HardwareHero.Shared.Models.Mail.MailMessage>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.MailCollection].CollectionName);

            _mapper = mapper;
            _configuration = configuration;
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
            await _mailCollection.InsertOneAsync(message);

            return messageToSend.Id;
        }
    }
}
