namespace HardwareHero.Shared.DTOs.Contributor
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string TimeStamp { get; set; }
        public ICollection<ChatMessageDto>? ChatMessages { get; set; } = new List<ChatMessageDto>();
        public ICollection<ContributorModelDto>? Participants { get; set; } = new List<ContributorModelDto>();
    }
}
