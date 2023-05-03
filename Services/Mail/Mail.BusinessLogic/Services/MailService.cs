using AutoMapper;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Mail;
using HardwareHero.Services.Shared.Models.Mail;
using HardwareHero.Services.Shared.Settings;
using Mail.BusinessLogic.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mail.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IMongoCollection<MailMessage> _mailCollection;
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _databaseSettings;

        public MailService(
            IOptions<DatabaseSettings> databaseSettings,
            IMapper mapper)
        {
            _databaseSettings = databaseSettings.Value;
            var mongoClient = new MongoClient(_databaseSettings.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(_databaseSettings.DatabaseName);

            _mailCollection = mongoDb
                .GetCollection<MailMessage>(
                _databaseSettings.Collections[ConfiguratorCollectionNames.MailCollection].CollectionName);

            _mapper = mapper;
        }

        public Task<bool> SendMessage(MailMessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
