using HardwareHero.Shared.Models;

namespace Mail.BusinessLogic.Models
{
    public class MailMessage : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Status { get; set; }
        public string RecipientMailAddress { get; set; }
        public Guid RecipientId { get; set; }
        public Guid? SenderId { get; set; }
    }
}
