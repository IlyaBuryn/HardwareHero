using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Contributor.BusinessLogic.Services
{
    public class ReferencesDataService : IReferencesDataService
    {
        private readonly ICrudRepositoryAsync<Region> _regionRepo;
        private readonly ICrudRepositoryAsync<Currency> _currencyRepo;

        private readonly IValidationRepository<Region> _regionValidationRepo;
        private readonly IValidationRepository<Currency> _currencyValidationRepo;

        private readonly IImageRepositoryAsync _imageRepo;

        private readonly IMapper _mapper;

        public ReferencesDataService(
            ICrudRepositoryAsync<Region> regionRepo,
            ICrudRepositoryAsync<Currency> currencyRepo,
            IValidationRepository<Region> regionValidationRepo,
            IValidationRepository<Currency> currencyValidationRepo,
            IImageRepositoryAsync imageRepo,
            IMapper mapper)
        {
            _regionRepo = regionRepo;
            _currencyRepo = currencyRepo;
            _regionValidationRepo = regionValidationRepo;
            _currencyValidationRepo = currencyValidationRepo;
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddRegionAsync(RegionDto regionToAdd)
        {
            regionToAdd.Id = Guid.NewGuid();

            _regionValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Country == regionToAdd.Country && x.City == regionToAdd.City);

            var region = _mapper.Map<Region>(regionToAdd);
            var result = await _regionRepo.CreateEntityAsync(region);

            return result;
        }

        public async Task<bool> UpdateRegionAsync(RegionDto regionToUpdate)
        {
            _regionValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Country == regionToUpdate.Country && x.City == regionToUpdate.City);

            var region = await _regionRepo.GetOneWithNotFoundCheck
                (x => x.Id == regionToUpdate.Id, false);

            region.City = regionToUpdate.City;
            region.Country = regionToUpdate.Country;

            var result = await _regionRepo.UpdateEntityAsync(region);

            return result;
        }

        public async Task<IEnumerable<string>?> GetCountriesAsync()
        {
            var countries = new List<string>();

            var countriesSet = await _regionRepo.GetManyEntitiesAsync();

            countries = await countriesSet.GroupBy(x => x.Country).Select(x => x.Key).ToListAsync();

            return countries;
        }

        public async Task<IEnumerable<RegionDto?>?> GetRegionsByCountryAsync(string country)
        {
            var regionsSet = await _regionRepo.GetManyWithDefaultOrEmptyCheckAsync(
                x => x.Country == country, false);

            var regionsList = await regionsSet.ToListAsync();
            var result = _mapper.Map<IEnumerable<RegionDto?>?>(regionsList);

            return result;
        }

        public async Task<RegionDto?> GetRegionByCityAsync(string city)
        {
            var region = await _regionRepo.GetOneWithNotFoundCheck(
                x => x.City == city, false);

            var result = _mapper.Map<RegionDto>(region);

            return result;
        }


        public async Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd)
        {
            currencyToAdd.Id = Guid.NewGuid();

            _currencyValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Name == currencyToAdd.Name);

            await _imageRepo.SaveImageAsync(currencyToAdd.ImageData, currencyToAdd.Icon, null);

            var currency = _mapper.Map<Currency>(currencyToAdd);
            var result = await _currencyRepo.CreateEntityAsync(currency);

            return result;
        }

        public async Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate)
        {
            _currencyValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Name == currencyToUpdate.Name);

            var currency = await _currencyRepo
                .GetOneWithNotFoundCheck(x => x.Id == currencyToUpdate.Id, false);

            currency.Name = currencyToUpdate.Name;
            currency.Icon = currencyToUpdate.Icon;

            await _imageRepo.UpdateImageAsync(currencyToUpdate.ImageData, currencyToUpdate.Icon, null);

            var result = await _currencyRepo.UpdateEntityAsync(currency);

            return result;
        }

        public async Task<IEnumerable<CurrencyDto?>?> GetCurrenciesAsync()
        {
            var currenciesSet = await _currencyRepo.GetManyEntitiesAsync();

            var currenciesList = await currenciesSet.ToListAsync();

            var result = _mapper.Map<IEnumerable<CurrencyDto?>?>(currenciesList);

            return result;
        }
    }
}
