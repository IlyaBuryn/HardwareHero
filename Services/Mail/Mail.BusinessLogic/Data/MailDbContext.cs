using HardwareHero.Shared.Repositories.Mongo;
using MongoDB.Driver;
using System.Net.Mail;

namespace Mail.BusinessLogic.Data
{
    public class MailDbContext : MongoDbContext
    {
        public IMongoCollection<MailMessage> Mails => Collection<MailMessage>("MailMessages");
    }
}
