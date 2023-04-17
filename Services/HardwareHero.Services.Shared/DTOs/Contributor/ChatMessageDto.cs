using HardwareHero.Services.Shared.Models.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public ContributorDto Sender { get; set; }
        public Guid SenderId { get; set; }
        public ChatRoomDto ChatRoom { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}
