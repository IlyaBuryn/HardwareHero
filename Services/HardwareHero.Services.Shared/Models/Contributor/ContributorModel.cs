using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class ContributorModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? ContributorConfirmInfoId { get; set; }
        public Guid ContributorExcellenceId { get; set; }
        public Guid? SubscriptionPlanInfoId { get; set; }

        public virtual ContributorConfirmInfo? ContributorConfirmInfo { get; set; }
        public virtual SubscriptionPlanInfo? SubscriptionPlanInfo { get; set; }
        public virtual ContributorExcellence ContributorExcellence { get; set; }

        public virtual ICollection<ChatRoom>? ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
