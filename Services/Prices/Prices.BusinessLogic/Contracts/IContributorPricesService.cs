using HardwareHero.Services.Shared.DTOs.Prices;

namespace Prices.BusinessLogic.Contracts
{
    public interface IContributorPricesService
    {
        Task<Guid?> AddContributorPriceAsync(ContributorPriceDto priceToAdd);
        Task<ContributorPriceDto?> GetLastPriceAsync(Guid componentId);
        Task<List<ContributorPriceDto?>> GetContributorPricesByComponentIdAsync(Guid componentId);
    }
}
