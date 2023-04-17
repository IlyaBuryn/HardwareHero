using HardwareHero.Services.Shared.Models.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class SubscriptionInfoDto
    {
        public Guid Id { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public SubscriptionPlanDto Plan { get; set; }
        public Guid PlanId { get; set; }
        public ContributorDto Contributor { get; set; }
        public Guid ContributorId { get; set; }
    }
}
