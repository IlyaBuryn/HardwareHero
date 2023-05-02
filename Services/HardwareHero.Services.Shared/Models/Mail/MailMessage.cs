using MongoDB.Bson.Serialization.Attributes;

namespace HardwareHero.Services.Shared.Models.Mail
{
    public class MailMessage
    {
        [BsonId]
        public Guid Id { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid SenderId { get; set; }
    }
}
