namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int DaysCount { get; set; }
        public int PriorityLevel { get; set; }
    }
}
