using References.DTOs.Domain;
using References.DTOs.Messages;

namespace References.BusinessLogic.Contracts
{
    public interface ICurrencyService
    {
        Task<CurrencyDto> AddCurrencyAsync(CurrencyDto currencyToAdd);
        Task<CurrencyDto> UpdateCurrencyAsync(CurrencyDto currencyToUpdate);
        Task<IEnumerable<CurrencyDto>?> GetCurrenciesAsync();
        Task<CurrencyDto?> FindCurrencyAsync(CurrencyRequestMessage filter);
    }
}
