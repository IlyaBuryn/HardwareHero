using References.DTOs.Domain;

namespace Contributor.Domain.DTO.Subscription
{
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public Guid? CurrencyId { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int PriorityLevel { get; set; }

        public CurrencyDto? Currency { get; set; }
    }
}
