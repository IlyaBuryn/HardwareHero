namespace Prices.DTOs.Prices
{
    public class ContributorComponentPricesDto
    {
        public Guid? Id { get; set; }
        public Guid ComponentId { get; set; }
        public Guid ContributorId { get; set; }
        public List<PriceStamp> Prices { get; set; } = new();
        public bool IsUnsupported { get; set; } = false;
    }
}
