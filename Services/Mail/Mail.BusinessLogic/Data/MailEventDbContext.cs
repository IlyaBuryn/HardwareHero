using HardwareHero.Shared.Repositories.Mongo;
using Mail.DTOs.Events;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Mail.BusinessLogic.Data
{
    public class MailEventDbContext : MongoDbContext
    {
        public MailEventDbContext(IConfiguration configuration)
            : base(configuration)
        {
            RegisterCollection<SendMailEvent>("MailEvent");
        }

        public IMongoCollection<SendMailEvent> MailEvents => GetCollection<SendMailEvent>();
    }
}
