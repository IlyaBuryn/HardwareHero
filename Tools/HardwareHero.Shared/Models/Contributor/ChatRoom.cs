namespace HardwareHero.Shared.Models.Contributor
{
    public class ChatRoom : BaseEntity
    {
        public string Subject { get; set; }
        public string TimeStamp { get; set; }
        public virtual ICollection<ChatMessage>? ChatMessages { get; set; } = new List<ChatMessage>();
        public virtual ICollection<ContributorModel>? Participants { get; set; } = new List<ContributorModel>();
    }
}