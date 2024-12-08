using References.BusinessLogic.Contracts;
using static References.DTOs.Responses.ReferencesResponseRecords;

namespace References.BusinessLogic.Services
{
    public class ReferencesService : IReferencesService
    {
        private readonly ICurrencyService _currencyService;
        private readonly IRegionService _regionService;

        public ReferencesService(
            ICurrencyService currencyService,
            IRegionService regionService)
        {
            _currencyService = currencyService;
            _regionService = regionService;
        }


        public async Task<GeoReferences> GetGeoReferencesAsync()
        {
            var currencies = await _currencyService.GetCurrenciesAsync();
            var regions = await _regionService.GetRegionsAsync();

            return new GeoReferences(regions, currencies);
        }
    }
}
