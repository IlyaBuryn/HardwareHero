using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Mail.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IMongoCollection<HardwareHero.Shared.Models.Mail.MailMessage> _mailCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseOptions _databaseSettings;
        private readonly string senderEmailAddress = "issaac.bishop@gmail.com";

        public MailService(
            IOptions<DatabaseOptions> databaseSettings,
            IMapper mapper)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _mailCollection = mongoDb
                .GetCollection<HardwareHero.Shared.Models.Mail.MailMessage>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.MailCollection].CollectionName);

            _mapper = mapper;
        }

        public Guid SendMessage(MailMessageDto message)
        {
            MimeMessage messageToDelivery = new MimeMessage();

            var uniqueGuid = Guid.NewGuid();
            messageToDelivery.From.Add(new MailboxAddress("HardwareHero.Management: " + uniqueGuid.ToString(), senderEmailAddress));
            messageToDelivery.To.Add(new MailboxAddress("Получатель", message.RecipientsEmailAddress));
            messageToDelivery.Subject = message.MessageTitle;
            messageToDelivery.Body = new TextPart("plain")
            {
                Text = message.MessageContent
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(senderEmailAddress, "...");
                client.Send(messageToDelivery);
                client.Disconnect(true);
            }

            return uniqueGuid;
        }
    }
}
