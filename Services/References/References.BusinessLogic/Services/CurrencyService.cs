using AutoMapper;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using References.BusinessLogic.Contracts;
using References.BusinessLogic.Models;
using References.DTOs.Domain;
using References.DTOs.Messages;
using System.Linq.Expressions;

namespace References.BusinessLogic.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IBaseRepositoryAsync<Currency> _currencyRepo;
        private readonly IMapper _mapper;

        public CurrencyService(
            IBaseRepositoryAsync<Currency> currencyRepo,
            IMapper mapper)
        {
            _currencyRepo = currencyRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd)
        {
            await _currencyRepo.AlreadyExistCheckAsync(
                x => x.Code == currencyToAdd.Code);

            var currency = _mapper.Map<Currency>(currencyToAdd);

            var result = await _currencyRepo.CreateEntityAsync(currency);
            result.DataAnswerCheck();

            return currency.Id;
        }


        public async Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate)
        {
            await _currencyRepo.AlreadyExistCheckAsync(
                x => x.Code == currencyToUpdate.Code);

            await _currencyRepo.NotFoundCheckAsync(
                x => x.Id == currencyToUpdate.Id);

            var currency = _mapper.Map<Currency>(currencyToUpdate);

            var result = await _currencyRepo.UpdateEntityAsync(currency);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<IEnumerable<CurrencyDto>?> GetCurrenciesAsync()
        {
            var currencies = await _currencyRepo.FindAllEntitiesAsync();
            currencies.DataAnswerCheck();

            var mappedCurrencies = _mapper.Map<IEnumerable<CurrencyDto>>(
                currencies.Value);

            return mappedCurrencies;
        }


        public async Task<CurrencyDto?> FindCurrencyAsync(CurrencyRequestMessage filter)
        {
            ArgumentNullException.ThrowIfNull(filter, nameof(filter));

            Expression<Func<Currency, bool>>? predicate = filter.ByCurrencyId.HasValue
                ? x => x.Id == filter.ByCurrencyId
                : !string.IsNullOrEmpty(filter.ByCode)
                    ? x => x.Code == filter.ByCode
                    : null;

            if (predicate == null)
            {
                return null;
            }

            var result = await _currencyRepo.FindEntityAsync(predicate);

            return result.Value != null
                ? _mapper.Map<CurrencyDto>(result.Value)
                : null;
        }
    }
}
