using Aggregator.BusinessLogic.Contracts;
using Aggregator.BusinessLogic.Filters;
using AutoMapper;
using HardwareHero.Filter.Extensions;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Infrastructure;
using HardwareHero.Services.Shared.Infrastructure.Reviews;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentReviewService : IComponentReviewService
    {
        private readonly ICollectionRepositoryAsync<ComponentLocalReview> _localReviewRepo;
        private readonly ICollectionRepositoryAsync<ComponentGlobalReview> _globalReviewRepo;

        private readonly IValidationRepository<ComponentLocalReview> _localReviewValidationRepo;
        private readonly IValidationRepository<ComponentGlobalReview> _globalReviewValidationRepo;

        private readonly IMapper _mapper;

        public ComponentReviewService(
            ICollectionRepositoryAsync<ComponentLocalReview> localReviewRepo,
            ICollectionRepositoryAsync<ComponentGlobalReview> globalReviewRepo,
            IValidationRepository<ComponentLocalReview> localReviewValidationRepo,
            IValidationRepository<ComponentGlobalReview> globalReviewValidationRepo,
            IMapper mapper)
        {
            _localReviewRepo = localReviewRepo;
            _globalReviewRepo = globalReviewRepo;
            _localReviewValidationRepo = localReviewValidationRepo;
            _globalReviewValidationRepo = globalReviewValidationRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddLocalReviewAsync(ComponentLocalReviewDto reviewToAdd)
        {
            reviewToAdd.Id = Guid.NewGuid();

            _localReviewValidationRepo.CheckIfObjectAlreadyExist(
                x => x.ComponentId == reviewToAdd.ComponentId && x.UserId == reviewToAdd.UserId,
                reviewToAdd.UserId.ToString());

            var review = _mapper.Map<ComponentLocalReview>(reviewToAdd);
            var result = await _localReviewRepo.CreateEntityAsync(review);

            return result;
        }


        public async Task<bool> UpdateLocalReviewAsync(ComponentLocalReviewDto reviewToAdd)
        {
            _localReviewValidationRepo.CheckIfObjectNotFound(
                x => x.Id ==reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.UserId == reviewToAdd.UserId);

            var review = await _localReviewRepo.GetOneWithNotFoundCheck(x => x.Id == reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.Rating = reviewToAdd.Rating;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _localReviewRepo.UpdateEntityAsync(review);

            return result;
        }


        public async Task<bool> RemoveLocalReviewAsync(Guid reviewId)
        {
            var review = await _localReviewRepo.GetOneWithNotFoundCheck(x => x.Id == reviewId);

            var result = await _localReviewRepo.RemoveEntityAsync(reviewId);

            return result;
        }


        public async Task<Guid?> AddGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            reviewToAdd.Id = Guid.NewGuid();

            _globalReviewValidationRepo.CheckIfObjectAlreadyExist(
                x => x.ComponentId == reviewToAdd.ComponentId && x.AuthorName == reviewToAdd.AuthorName,
                reviewToAdd.AuthorName);

            var review = _mapper.Map<ComponentGlobalReview>(reviewToAdd);
            var result = await _globalReviewRepo.CreateEntityAsync(review);

            return result;
        }


        public async Task<bool> UpdateGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            _globalReviewValidationRepo.CheckIfObjectNotFound(
                x => x.Id == reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.AuthorName == reviewToAdd.AuthorName);

            var review = await _globalReviewRepo.GetOneWithNotFoundCheck(x => x.Id == reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.Rating = reviewToAdd.Rating;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _globalReviewRepo.UpdateEntityAsync(review);

            return result;
        }


        public async Task<bool> RemoveGlobalReviewAsync(Guid reviewId)
        {
            var review = await _globalReviewRepo.GetOneWithNotFoundCheck(x => x.Id == reviewId);

            var result = await _globalReviewRepo.RemoveEntityAsync(reviewId);

            return result;
        }


        public async Task<ComplexResponse> AddGlobalReviewsAsync(List<ComponentGlobalReviewDto> reviews)
        {
            var result = new ComplexResponse();

            foreach (var review in reviews)
            {
                try
                {
                    await AddGlobalReviewAsync(review);
                    result.Responses.Add(new ComplexResponse.TupleResponse(review.AuthorName, true.ToString()));
                }
                catch (Exception ex)
                {
                    result.Responses.Add(new ComplexResponse.TupleResponse(review.AuthorName, ex.Message));
                }
            }

            return result;
        }


        public async Task<PageResponse<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByComponentIdAsync(
            ComponentLocalReviewFilter filter, Guid componentId)
        {
            var paginationInfo = PaginationInfo.ConvertFromFilterPagination(filter.PageRequestInfo);
            _localReviewValidationRepo.CheckPaginationOptions(paginationInfo);

            var reviews = await _localReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);

            reviews = reviews.ApplySelection(filter).Query;

            var result = await _localReviewRepo.GetMappedPageAsync<ComponentLocalReviewDto>(
                reviews, paginationInfo, _mapper);

            return result;
        }


        public async Task<PageResponse<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByComponentIdAsync(
            ComponentGlobalReviewFilter filter, Guid componentId)
        {
            var paginationInfo = PaginationInfo.ConvertFromFilterPagination(filter.PageRequestInfo);
            _globalReviewValidationRepo.CheckPaginationOptions(paginationInfo);

            var reviews = await _globalReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);

            reviews = reviews.ApplySelection(filter).Query;

            var result = await _globalReviewRepo.GetMappedPageAsync<ComponentGlobalReviewDto>(
                reviews, paginationInfo, _mapper);

            return result;
        }


        public async Task<AvgReviewsMarksResponse> GetComponentAvgMarkAsync(Guid componentId)
        {
            var componentLocalReviews = await _localReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);
            var componentGlobalReviews = await _globalReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);

            var TotalGlobalReviewCount = componentGlobalReviews.Count();
            var TotalLocalReviewCount = componentLocalReviews.Count();

            var AvgGlobalReviewMark = CalculateAvgMark(componentGlobalReviews);
            var AvgLocalReviewMark = CalculateAvgMark(componentLocalReviews);
            var AvgReviewMark = AvgGlobalReviewMark == 0 ? 
                (AvgLocalReviewMark == 0 ? 0 : AvgLocalReviewMark) :
                (AvgGlobalReviewMark + AvgLocalReviewMark) / 2;

            var LocalReviewRecommendations = CalculateRecommendations(componentLocalReviews);
            var LocalReviewRatings = CalculateRatings(componentLocalReviews);

            var result = new AvgReviewsMarksResponse()
            {
                AvgGlobalReviewMark = AvgGlobalReviewMark,
                AvgLocalReviewMark = AvgLocalReviewMark,
                AvgReviewMark = AvgReviewMark,

                TotalGlobalReviewCount = TotalGlobalReviewCount,
                TotalLocalReviewCount = TotalLocalReviewCount,
                TotalReviewCount = TotalGlobalReviewCount + TotalLocalReviewCount,

                LocalReviewRecommendations = LocalReviewRecommendations,
                LocalReviewRatings = LocalReviewRatings,
            };

            return result;
        }


        protected decimal CalculateAvgMark(IQueryable<ReviewBase?> reviews)
        {
            if (reviews.Count() == 0)
            {
                return 0;
            }

            var totalRecommended = reviews.Where(x => x != null && x.IsRecommended != null);
            var totalTrueRecommended = totalRecommended.Count(x => x.IsRecommended == true);
            return (decimal)totalTrueRecommended / totalRecommended.Count() * 100;
        }


        protected Dictionary<bool, int> CalculateRecommendations(IQueryable<ReviewBase?> reviews)
        {
            return new Dictionary<bool, int>
            {
                { true, reviews.Count(x => x != null && x.IsRecommended == true) },
                { false, reviews.Count(x => x != null && x.IsRecommended == false) }
            };
        }


        protected Dictionary<int, int> CalculateRatings(IQueryable<ReviewBase?> reviews)
        {
            var ratings = Enumerable.Range(1, 5);
            return ratings.ToDictionary(rating => rating, rating => reviews.Count(x => x != null && x.Rating == rating));
        }
    }
}
