using Contributor.Domain.DTO.Subscription;

namespace Contributor.Domain.DTO.Contributors
{
    public class ContributorUserAccountDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? UserId { get; set; }
        public Guid? ContributorSubscriptionPlanId { get; set; }

        public bool IsVerified { get; set; } = false;
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public DateTime VerifiedDate { get; set; }
        public Guid ContributorId { get; set; }

        public bool IsBlocked { get; set; } = false;

        public ContributorSubscriptionPlanDto? ContributorSubscriptionPlan { get; set; }
            = new();
    }
}
