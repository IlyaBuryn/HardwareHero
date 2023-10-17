using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Newtonsoft.Json;

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
            _localReviewValidationRepo.CheckIsAlreadyExist(
                x => x.ComponentId == reviewToAdd.ComponentId &&
                x.UserId == reviewToAdd.UserId,
                new AlreadyExistException(nameof(reviewToAdd), reviewToAdd.UserId.ToString()));

            var review = _mapper.Map<ComponentLocalReview>(reviewToAdd);
            var result = await _localReviewRepo.CreateEntityAsync(review);

            return result;
        }


        public async Task<bool> UpdateLocalReviewAsync(ComponentLocalReviewDto reviewToAdd)
        {
            _localReviewValidationRepo.CheckIsNotFound(
                x => x.Id ==reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.UserId == reviewToAdd.UserId,
                new NotFoundException(nameof(reviewToAdd)));

            var review = await _localReviewRepo.GetOneEntityAsync(reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.Rating = reviewToAdd.Rating;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _localReviewRepo.UpdateEntityAsync(review);

            return result;
        }


        public async Task<bool> RemoveLocalReviewAsync(Guid reviewId)
        {
            var review = await _localReviewRepo.GetOneEntityAsync(reviewId);
            if (review == null)
            {
                throw new NotFoundException(nameof(review));
            }

            var result = await _localReviewRepo.RemoveEntityAsync(reviewId);

            return result;
        }


        public async Task<Guid?> AddGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            reviewToAdd.Id = Guid.NewGuid();
            _globalReviewValidationRepo.CheckIsAlreadyExist(
                x => x.ComponentId == reviewToAdd.ComponentId &&
                x.AuthorName == reviewToAdd.AuthorName,
                new AlreadyExistException(nameof(reviewToAdd), reviewToAdd.AuthorName));

            var review = _mapper.Map<ComponentGlobalReview>(reviewToAdd);
            var result = await _globalReviewRepo.CreateEntityAsync(review);

            return result;
        }


        public async Task<bool> UpdateGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            _globalReviewValidationRepo.CheckIsNotFound(
                x => x.Id == reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.AuthorName == reviewToAdd.AuthorName,
                new NotFoundException(nameof(reviewToAdd)));

            var review = await _globalReviewRepo.GetOneEntityAsync(reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.Rating = reviewToAdd.Rating;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _globalReviewRepo.UpdateEntityAsync(review);

            return result;
        }


        public async Task<bool> RemoveGlobalReviewAsync(Guid reviewId)
        {
            var review = await _globalReviewRepo.GetOneEntityAsync(reviewId);
            if (review == null)
            {
                throw new NotFoundException(nameof(review));
            }

            var result = await _globalReviewRepo.RemoveEntityAsync(reviewId);

            return result;
        }


        public async Task<Guid[]> AddGlobalReviewsFromJsonAsync(string jsonData)
        {
            var reviewsToAdd = JsonConvert.DeserializeObject<List<ComponentGlobalReviewDto>>(jsonData);

            if (reviewsToAdd == null || !reviewsToAdd.Any())
            {
                return new Guid[0];
            }

            var resultTotal = new List<Guid>();

            foreach (var review in reviewsToAdd)
            {
                var reviewId = await AddGlobalReviewAsync(review);
                if (reviewId.HasValue)
                {
                    resultTotal.Add(reviewId.Value);
                }
            }

            return resultTotal.ToArray();
        }


        public async Task<PageResponse<ComponentLocalReviewDto?>> GetComponentLocalReviewsAsPageByComponentIdAsync(
            PaginationInfo paginationInfo, Guid componentId)
        {
            _localReviewValidationRepo.CheckPaginationOptions(paginationInfo, new PageOptionsValidationException());

            var reviews = await _localReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);

            var result = await _localReviewRepo.GetMappedPageAsync<ComponentLocalReviewDto>(
                reviews, paginationInfo, _mapper);

            return result;
        }


        public async Task<PageResponse<ComponentGlobalReviewDto?>> GetComponentGlobalReviewsAsPageByComponentIdAsync(
            PaginationInfo paginationInfo, Guid componentId)
        {
            _globalReviewValidationRepo.CheckPaginationOptions(paginationInfo, new PageOptionsValidationException());

            var reviews = await _globalReviewRepo.GetManyEntitiesAsync(x => x.ComponentId == componentId);

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
            var AvgReviewMark = (AvgGlobalReviewMark + AvgLocalReviewMark) / 2;

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


        protected decimal CalculateAvgMark(IEnumerable<ReviewBase?> reviews)
        {
            if (reviews.Count() == 0)
                return 0;

            var totalRecommended = reviews.Count(x => x != null && x.IsRecommended == true);
            return (decimal)totalRecommended / reviews.Count() * 100;
        }


        protected Dictionary<bool, int> CalculateRecommendations(IEnumerable<ReviewBase?> reviews)
        {
            return new Dictionary<bool, int>
            {
                { true, reviews.Count(x => x != null && x.IsRecommended == true) },
                { false, reviews.Count(x => x != null && x.IsRecommended == false) }
            };
        }


        protected Dictionary<int, int> CalculateRatings(IEnumerable<ReviewBase?> reviews)
        {
            var ratings = Enumerable.Range(1, 5);
            return ratings.ToDictionary(rating => rating, rating => reviews.Count(x => x != null && x.Rating == rating));
        }
    }
}
