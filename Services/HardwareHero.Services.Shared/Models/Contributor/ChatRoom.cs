namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class ChatRoom : BaseEntity
    {
        public virtual ICollection<ContributorModel> Contributors { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}