using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class ContributorModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? ContributorConfirmInfoId { get; set; }
        public Guid ContributorExcellenceId { get; set; }
        public Guid? SubscriptionPlanInfoId { get; set; }
        
        // TODO: To migrations
        public DateTime ContributorApplicationDate { get; set; } = DateTime.UtcNow;

        public virtual ContributorConfirmInfo? ContributorConfirmInfo { get; set; }
        public virtual SubscriptionPlanInfo? SubscriptionPlanInfo { get; set; }
        public virtual ContributorExcellence ContributorExcellence { get; set; }

        public virtual ICollection<ChatRoom>? ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
