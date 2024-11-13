namespace Prices.DTOs.Responses
{
    public class PricesResponseRecords
    {
        public record PositionsResponse(
            Guid contributorId,
            decimal price);

        public record PriceResponse(
            Guid currencyId,
            decimal price);
    }
}
