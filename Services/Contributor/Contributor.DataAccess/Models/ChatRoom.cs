using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class ChatRoom : BaseEntity
    {
        public string Subject { get; set; }
        public string TimeStamp { get; set; }
        public virtual ICollection<ChatMessage>? ChatMessages { get; set; } 
            = new List<ChatMessage>();
        public virtual ICollection<ContributorModel>? Contributors { get; set; } 
            = new List<ContributorModel>();

    }
}
