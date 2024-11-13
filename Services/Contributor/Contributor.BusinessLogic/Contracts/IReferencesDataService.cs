using Contributor.DTOs.Domain.Currencies;
using Contributor.DTOs.Domain.Regions;
using static Contributor.DTOs.Responses.ContributorResponseRecords;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IReferencesDataService
    {
        Task<Guid?> AddRegionAsync(RegionDto regionToAdd);
        Task<bool> UpdateRegionAsync(RegionDto regionToUpdate);
        Task<IEnumerable<RegionDto?>?> GetRegionsAsync();
        Task<IEnumerable<CityDto?>?> GetCitiesByCountryAsync(string countryCode);
        Task<IEnumerable<RegionDto?>?> GetRegionsByCountryAsync(string countryCode);
        Task<RegionDto?> GetRegionByCityAsync(string city);

        Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd);
        Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate);
        Task<IEnumerable<CurrencyDto?>?> GetCurrenciesAsync();

        Task<RegionsAndCurrenciesResponse> GetCurrenciesAndRegionsAsync();
    }
}
