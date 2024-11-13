using Contributor.DTOs.Domain.Contributors;
using System.Text.Json.Serialization;

namespace Contributor.DTOs.Domain.Chat
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsEdited { get; set; } = false;
        public DateTime Timestamp { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }

        [JsonIgnore]
        public ChatRoomDto? ChatRoom { get; set; }
        [JsonIgnore]
        public ContributorModelDto? Sender { get; set; }
    }
}
