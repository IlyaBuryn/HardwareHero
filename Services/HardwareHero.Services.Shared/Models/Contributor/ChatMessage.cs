namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class ChatMessage : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ContributorModel Sender { get; set; }
        public Guid SenderId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}