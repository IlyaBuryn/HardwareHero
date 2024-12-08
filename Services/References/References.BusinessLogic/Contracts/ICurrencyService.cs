using References.DTOs.Domain;
using References.DTOs.Messages;

namespace References.BusinessLogic.Contracts
{
    public interface ICurrencyService
    {
        Task<Guid?> AddCurrencyAsync(CurrencyDto currencyToAdd);
        Task<bool> UpdateCurrencyAsync(CurrencyDto currencyToUpdate);
        Task<IEnumerable<CurrencyDto>?> GetCurrenciesAsync();
        Task<CurrencyDto?> FindCurrencyAsync(CurrencyRequestMessage filter);
    }
}
