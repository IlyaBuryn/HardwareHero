using HardwareHero.Filter.RequestsModels;
using HardwareHero.Shared.Models;
using HardwareHero.Shared.Requests;
using HardwareHero.Shared.Responses;

namespace Prices.BusinessLogic.Contracts
{
    public interface IContributorPricesService
    {
        Task<Guid?> ChangePriceAsync(ChangePriceRequest priceToAdd);
        Task<List<ContributorComponentPricesDto?>> GetComponentPricesAsync(Guid componentId);

        //Task<PageResponse<ContributorComponentPricesDto?>> GetPricesToDiscreetlyUpdate(PaginationInfo pageInfo);
    }
}
