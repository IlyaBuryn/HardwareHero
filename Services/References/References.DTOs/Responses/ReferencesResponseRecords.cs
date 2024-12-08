using References.DTOs.Domain;

namespace References.DTOs.Responses
{
    public class ReferencesResponseRecords
    {
        public record GeoReferences(
            IEnumerable<RegionDto>? regions,
            IEnumerable<CurrencyDto>? currencies);
    }
}
