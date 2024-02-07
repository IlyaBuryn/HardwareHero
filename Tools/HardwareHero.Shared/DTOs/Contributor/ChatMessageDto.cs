namespace HardwareHero.Shared.DTOs.Contributor
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsEdited { get; set; } = false;
        public DateTime Timestamp { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }
        public ChatRoomDto? ChatRoom { get; set; }
        public ContributorModelDto? Sender { get; set; }
    }
}
