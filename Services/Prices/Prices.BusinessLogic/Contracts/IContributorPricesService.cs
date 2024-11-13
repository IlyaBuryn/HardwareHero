using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Responses;
using static Prices.DTOs.Requests.PricesRequests;
using static Prices.DTOs.Responses.PricesResponseRecords;

namespace Prices.BusinessLogic.Contracts
{
    public interface IContributorPricesService
    {
        Task<Guid?> ChangePriceAsync(ChangePriceRequest priceToAdd);
        Task<PageResponse<PositionsResponse>> GetPositionsPagedAsync(Guid componentId, IPaginable filter);
        Task<PriceResponse> GetLowestFromLatestPricesAsync(Guid componentId);
        Task<bool> ChangeUnsupportedStatusAsync(Guid componentPriceId);
    }
}
