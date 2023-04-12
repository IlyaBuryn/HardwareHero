using HardwareHero.Services.Shared.Models.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public ICollection<ContributorDto> Contributors { get; set; }
        public ICollection<ChatMessageDto> ChatMessages { get; set; }
    }
}
