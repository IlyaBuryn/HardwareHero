using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Responses;
using MongoDB.Driver;
using Prices.BusinessLogic.Models;
using Prices.DTOs.Prices;
using static Prices.DTOs.Requests.PricesRequests;
using static Prices.DTOs.Responses.PricesResponseRecords;

namespace Prices.BusinessLogic.Services
{
    public class ContributorPriceService : IContributorPricesService
    {
        private readonly IBaseRepositoryAsync<ContributorComponentPrices> _pricesRepo;
        private readonly IMapper _mapper;


        public ContributorPriceService(
            IBaseRepositoryAsync<ContributorComponentPrices> pricesRepo,
            IMapper mapper)
        {
            _pricesRepo = pricesRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> ChangePriceAsync(ChangePriceRequest request)
        {
            var price = await _pricesRepo.FindEntityAsync(
                x => x.ComponentId == request.componentId &&
                x.ContributorId == request.contributorId);
            price.DataAnswerCheck();

            if (price.Value == null)
            {
                var newPrice = new ContributorComponentPrices()
                {
                    Id = Guid.NewGuid(),
                    ComponentId = request.componentId,
                    ContributorId = request.contributorId,
                    Prices = new List<PriceStamp>()
                    {
                        new()
                        {
                            Price = request.newPrice,
                            Timestamp = DateTime.Now,
                        }
                    }
                };

                var created = await _pricesRepo.CreateEntityAsync(newPrice);
                created.DataAnswerCheck();

                return created.Value!.Id;
            }

            price.Value.Prices.Add(new PriceStamp()
            {
                Price = request.newPrice,
                Timestamp = DateTime.Now,
            });

            var updated = await _pricesRepo.UpdateEntityAsync(price.Value);
            updated.DataAnswerCheck();

            return updated.Value!.Id;
        }

        public async Task<PageResponse<PositionsResponse>> GetPositionsPagedAsync(Guid componentId, IPaginable filter)
        {
            // TODO: Need to edit IQueryRepo for mongoDb
            throw new NotImplementedException();
            //var prices = await _pricesRepo.FindAllEntitiesAsync(
            //    x => x.ComponentId == componentId);
            //prices.DataAnswerCheck();

            //var result = _mapper.Map<List<ContributorComponentPricesDto?>>(prices.Value!);

            //return result;
        }

        public async Task<PriceResponse> GetLowestFromLatestPricesAsync(Guid componentId)
        {
            var pricesAnswer = await _pricesRepo.FindAllEntitiesAsync(
                x => x.ComponentId == componentId && x.IsUnsupported == false);
            pricesAnswer.DataAnswerCheck();

            var prices = pricesAnswer.Value;
            if (prices == null)
            {
                throw new NullReferenceException(nameof(prices));
            }

            var latestLowPrice = prices.Select(x => x.Prices.Last().Price).Min();

            // TODO: Change NewGuid to currencyId.
            return new PriceResponse(Guid.NewGuid(), latestLowPrice);
        }

        public async Task<bool> ChangeUnsupportedStatusAsync(Guid componentPriceId)
        {
            var price = await _pricesRepo.NotFoundCheckAsync(
                x => x.Id == componentPriceId);

            price.IsUnsupported = !price.IsUnsupported;
            var result = await _pricesRepo.UpdateEntityAsync(price);

            return result.Value != null;
        }

        //public async Task<PageResponse<ContributorComponentPricesDto?>> GetPricesToDiscreetlyUpdate(PaginationInfo pageInfo)
        //{
        //    var filter = Builders<ContributorComponentPrices>.Filter.Empty;
        //    long totalCount = await _pricesCollection.CountDocumentsAsync(filter);
        //    int totalPages = (int)Math.Ceiling((double)totalCount / pageInfo.PageSize);
        //    int skip = (pageInfo.PageNumber - 1) * pageInfo.PageSize;

        //    var options = new FindOptions<ContributorComponentPrices>
        //    {
        //        Limit = pageInfo.PageSize,
        //        Skip = skip
        //    };

        //    var prices = await _pricesCollection.FindAsync(filter, options);
        //    var pricesList = _mapper.Map<List<ContributorComponentPricesDto?>>(prices.ToList());
        //    var result = new PageResponse<ContributorComponentPricesDto>()
        //    {
        //        CurrentPaginationInfo = pageInfo,
        //        Items = pricesList,
        //        TotalPages = totalPages
        //    };

        //    return result;
        //}
    }
}
