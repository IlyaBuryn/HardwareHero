namespace HardwareHero.Shared.Models.Contributor
{
    public class ChatMessage : BaseEntity
    {
        public string Text { get; set; }
        public bool IsEdited { get; set; } = false;
        public DateTime Timestamp { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }
        public virtual ChatRoom? ChatRoom { get; set; }
        public virtual ContributorModel? Sender { get; set; }
    }
}