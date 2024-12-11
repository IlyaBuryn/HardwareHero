using Contributor.Domain.DTO.Contributors;
using System.Text.Json.Serialization;

namespace Contributor.Domain.DTO.Chat
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Subject { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;

        public ICollection<ChatMessageDto>? ChatMessages { get; set; } 
            = new List<ChatMessageDto>();

        [JsonIgnore]
        public ICollection<ContributorModelDto>? Participants { get; set; } 
            = new List<ContributorModelDto>();
    }
}
