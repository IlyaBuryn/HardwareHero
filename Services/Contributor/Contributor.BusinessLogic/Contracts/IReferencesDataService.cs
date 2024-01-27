using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IReferencesDataService
    {
        Task<Guid?> AddRegionAsync(RegionDto regionToAdd);
        Task<bool> UpdateRegionAsync(RegionDto regionToUpdate);
        Task<IEnumerable<string>?> GetCountriesAsync();
        Task<IEnumerable<RegionDto?>?> GetRegionsByCountryAsync(string country);
        Task<RegionDto?> GetRegionByCityAsync(string city);

        Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd);
        Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate);
        Task<IEnumerable<CurrencyDto?>?> GetCurrenciesAsync();
    }
}
