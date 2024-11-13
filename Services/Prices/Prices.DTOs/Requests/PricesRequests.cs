namespace Prices.DTOs.Requests
{
    public class PricesRequests
    {
        public record ChangePriceRequest(Guid componentId, Guid contributorId, decimal newPrice);
    }
}
