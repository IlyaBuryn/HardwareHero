using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Currencies;
using Contributor.DTOs.Domain.Regions;
using HardwareHero.Shared.Extensions.Repository;
using Microsoft.EntityFrameworkCore;
using static Contributor.DTOs.Responses.ContributorResponseRecords;

namespace Contributor.BusinessLogic.Services
{
    public class ReferencesDataService : IReferencesDataService
    {
        private readonly IBaseRepositoryAsync<Region> _regionRepo;
        private readonly IBaseRepositoryAsync<Currency> _currencyRepo;

        private readonly IMapper _mapper;

        public ReferencesDataService(
            IBaseRepositoryAsync<Region> regionRepo,
            IBaseRepositoryAsync<Currency> currencyRepo,
            IMapper mapper)
        {
            _regionRepo = regionRepo;
            _currencyRepo = currencyRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddRegionAsync(RegionDto regionToAdd)
        {
            regionToAdd.Id = Guid.NewGuid();

            await _regionRepo.AlreadyExistCheckAsync(
                x => x.Country == regionToAdd.Country && x.City == regionToAdd.City);

            var region = _mapper.Map<Region>(regionToAdd);
            var result = await _regionRepo.CreateEntityAsync(region);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }

        public async Task<bool> UpdateRegionAsync(RegionDto regionToUpdate)
        {
            await _regionRepo.AlreadyExistCheckAsync(
                x => x.Country == regionToUpdate.Country && x.City == regionToUpdate.City);

            var region = await _regionRepo.NotFoundCheckAsync(x => x.Id == regionToUpdate.Id);

            region.City = regionToUpdate.City;
            region.Country = regionToUpdate.Country;

            var result = await _regionRepo.UpdateEntityAsync(region);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<IEnumerable<RegionDto?>?> GetRegionsAsync()
        {
            var regions = await _regionRepo.FindAllEntitiesAsync();
            regions.DataAnswerCheck();

            var result = _mapper.Map<List<RegionDto?>>(regions.Value);

            return result;
        }

        public async Task<IEnumerable<RegionDto?>?> GetRegionsByCountryAsync(string countryCode)
        {
            var regions = await _regionRepo.FindAllEntitiesAsync(x => x.Code == countryCode);
            regions.DataAnswerCheck();

            var result = _mapper.Map<IEnumerable<RegionDto?>?>(regions.Value);

            return result;
        }

        public async Task<IEnumerable<CityDto?>?> GetCitiesByCountryAsync(string countryCode)
        {
            var regions = await _regionRepo.FindAllEntitiesAsync(x => x.Code == countryCode);
            regions.DataAnswerCheck();

            var result = _mapper.Map<IEnumerable<CityDto?>?>(regions.Value);

            return result;
        }

        public async Task<RegionDto?> GetRegionByCityAsync(string city)
        {
            var region = await _regionRepo.FindEntityAsync(x => x.City == city);

            var result = _mapper.Map<RegionDto>(region.Value);

            return result;
        }


        public async Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd)
        {
            currencyToAdd.Id = Guid.NewGuid();

            await _currencyRepo.AlreadyExistCheckAsync(
                x => x.Code == currencyToAdd.Code);

            var currency = _mapper.Map<Currency>(currencyToAdd);
            var result = await _currencyRepo.CreateEntityAsync(currency);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }

        public async Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate)
        {
            await _currencyRepo.AlreadyExistCheckAsync(x => x.Code == currencyToUpdate.Code);

            var currency = await _currencyRepo
                .NotFoundCheckAsync(x => x.Id == currencyToUpdate.Id);

            currency.Code = currencyToUpdate.Code;
            currency.Symbol = currencyToUpdate.Symbol;

            var result = await _currencyRepo.UpdateEntityAsync(currency);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<IEnumerable<CurrencyDto?>?> GetCurrenciesAsync()
        {
            var currenciesSet = await _currencyRepo.FindAllEntitiesAsync();
            var result = _mapper.Map<IEnumerable<CurrencyDto?>?>(currenciesSet.Value);

            return result;
        }

        public async Task<RegionsAndCurrenciesResponse> GetCurrenciesAndRegionsAsync()
        {
            var regions = await GetRegionsAsync();
            var currencies = await GetCurrenciesAsync();

            return new RegionsAndCurrenciesResponse(
                regions, currencies);
        }
    }
}
