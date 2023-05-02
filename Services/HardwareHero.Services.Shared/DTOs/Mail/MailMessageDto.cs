namespace HardwareHero.Services.Shared.DTOs.Mail
{
    public class MailMessageDto
    {
        public Guid Id { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid SenderId { get; set; }
    }
}
