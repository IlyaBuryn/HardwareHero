using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Responses;
using Prices.DTOs.Events;
using static Prices.DTOs.Requests.PricesRequests;
using static Prices.DTOs.Responses.PricesResponseRecords;

namespace Prices.BusinessLogic.Contracts
{
    public interface IContributorPricesService
    {
        Task<Guid?> ChangePriceAsync(ChangePriceRequest priceToAdd);
        Task<PageResponse<PositionsResponse>> GetPositionsPagedAsync(Guid componentId, IPaginable filter);
        Task<LatestLowestComponentPriceEvent> GetLowestFromLatestPricesAsync(Guid componentId);
        Task<bool> ChangeUnsupportedStatusAsync(Guid componentPriceId);
    }
}
