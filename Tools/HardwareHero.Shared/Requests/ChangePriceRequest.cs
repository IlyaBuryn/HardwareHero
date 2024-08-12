namespace HardwareHero.Shared.Requests
{
    public class ChangePriceRequest
    {
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
