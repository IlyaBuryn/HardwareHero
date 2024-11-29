namespace Prices.DTOs.Responses
{
    public class PricesResponseRecords
    {
        public record PositionsResponse(
            Guid contributorId,
            decimal price);
    }
}
