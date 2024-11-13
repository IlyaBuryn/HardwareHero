using Contributor.DTOs.Domain.Currencies;
using Contributor.DTOs.Domain.Regions;

namespace Contributor.DTOs.Responses
{
    public class ContributorResponseRecords
    {
        public record RegionsAndCurrenciesResponse(
            IEnumerable<RegionDto?>? regions,
            IEnumerable<CurrencyDto?>? currencies);
    }
}
