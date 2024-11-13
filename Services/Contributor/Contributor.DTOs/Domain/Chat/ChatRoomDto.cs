using Contributor.DTOs.Domain.Contributors;
using System.Text.Json.Serialization;

namespace Contributor.DTOs.Domain.Chat
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string TimeStamp { get; set; }
        public ICollection<ChatMessageDto>? ChatMessages { get; set; } = new List<ChatMessageDto>();

        [JsonIgnore]
        public ICollection<ContributorModelDto>? Participants { get; set; } = new List<ContributorModelDto>();
    }
}
