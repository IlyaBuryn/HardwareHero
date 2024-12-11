using Contributor.Domain.DTO.Contributors;
using System.Text.Json.Serialization;

namespace Contributor.Domain.DTO.Chat
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Text { get; set; }
        public bool? IsEdited { get; set; } = false;
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }

        [JsonIgnore]
        public ChatRoomDto? ChatRoom { get; set; }
        [JsonIgnore]
        public ContributorModelDto? Sender { get; set; }
    }
}
