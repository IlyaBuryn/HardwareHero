using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using HardwareHero.Shared.Extensions.Repository;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentReviewService : IComponentReviewService
    {
        private readonly IQueryRepositoryAsync<ComponentLocalReview> _localReviewRepo;
        private readonly IQueryRepositoryAsync<ComponentGlobalReview> _globalReviewRepo;

        private readonly IMapper _mapper;

        public ComponentReviewService(
            IQueryRepositoryAsync<ComponentLocalReview> localReviewRepo,
            IQueryRepositoryAsync<ComponentGlobalReview> globalReviewRepo,
            IMapper mapper)
        {
            _localReviewRepo = localReviewRepo;
            _globalReviewRepo = globalReviewRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> AddLocalReviewAsync(ComponentLocalReviewDto reviewToAdd)
        {
            reviewToAdd.Id = Guid.NewGuid();

            await _localReviewRepo.AlreadyExistCheckAsync(
                x => x.ComponentId == reviewToAdd.ComponentId && x.UserId == reviewToAdd.UserId);

            var review = _mapper.Map<ComponentLocalReview>(reviewToAdd);
            var result = await _localReviewRepo.CreateEntityAsync(review);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> UpdateLocalReviewAsync(ComponentLocalReviewDto reviewToAdd)
        {
            await _localReviewRepo.NotFoundCheckAsync(
                x => x.Id == reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.UserId == reviewToAdd.UserId);

            var review = await _localReviewRepo.NotFoundCheckAsync(x => x.Id == reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _localReviewRepo.UpdateEntityAsync(review);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveLocalReviewAsync(Guid reviewId)
        {
            var review = await _localReviewRepo.NotFoundCheckAsync(x => x.Id == reviewId);

            var result = await _localReviewRepo.RemoveEntityAsync(reviewId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<Guid?> AddGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            reviewToAdd.Id = Guid.NewGuid();

            await _globalReviewRepo.AlreadyExistCheckAsync(
                x => x.ComponentId == reviewToAdd.ComponentId && x.AuthorName == reviewToAdd.AuthorName);

            var review = _mapper.Map<ComponentGlobalReview>(reviewToAdd);
            var result = await _globalReviewRepo.CreateEntityAsync(review);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> UpdateGlobalReviewAsync(ComponentGlobalReviewDto reviewToAdd)
        {
            await _globalReviewRepo.NotFoundCheckAsync(
                x => x.Id == reviewToAdd.Id &&
                x.ComponentId == reviewToAdd.ComponentId &&
                x.AuthorName == reviewToAdd.AuthorName);

            var review = await _globalReviewRepo.NotFoundCheckAsync(x => x.Id == reviewToAdd.Id);

            review.Text = reviewToAdd.Text;
            review.IsRecommended = reviewToAdd.IsRecommended;

            var result = await _globalReviewRepo.UpdateEntityAsync(review);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveGlobalReviewAsync(Guid reviewId)
        {
            var review = await _globalReviewRepo.NotFoundCheckAsync(x => x.Id == reviewId);

            var result = await _globalReviewRepo.RemoveEntityAsync(reviewId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<CreationOfManyResponse> AddGlobalReviewsAsync(List<ComponentGlobalReviewDto> reviews)
        {
            var values = new Dictionary<string, string>();

            foreach (var review in reviews)
            {
                try
                {
                    await AddGlobalReviewAsync(review);
                    values.Add(review.AuthorName, true.ToString());
                }
                catch (Exception ex)
                {
                   values.Add(review.AuthorName, ex.Message);
                }
            }

            return new CreationOfManyResponse(values);
        }


        public async Task<PageResponse<ComponentLocalReviewDto>?> GetComponentLocalReviewsPageAsync(
            ComponentLocalReviewFilter filter, Guid componentId)
        {
            var reviews = await _localReviewRepo.FindPagedAsync(
                x => x.ComponentId == componentId, filter);
            reviews.DataAnswerCheck();

            var page = reviews.ToPageResponse();
            reviews = null;
            var mappedResult = _mapper.Map<PageResponse<ComponentLocalReviewDto>>(page);

            return mappedResult;
        }


        public async Task<PageResponse<ComponentGlobalReviewDto>?> GetComponentGlobalReviewsPageAsync(
            ComponentGlobalReviewFilter filter, Guid componentId)
        {
            var reviews = await _globalReviewRepo.FindPagedAsync(
                x => x.ComponentId == componentId, filter);
            reviews.DataAnswerCheck();

            var page = reviews.ToPageResponse();
            reviews = null;
            var mappedResult = _mapper.Map<PageResponse<ComponentGlobalReviewDto>>(page);

            return mappedResult;
        }


        public async Task<ReviewsMetricResponse> GetReviewsMetricForComponent(Guid componentId)
        {
            var localCount = await _localReviewRepo.CountAsync(x => x.ComponentId == componentId);
            var globalCount = await _globalReviewRepo.CountAsync(x => x.ComponentId == componentId);

            var positiveLocal = await _localReviewRepo.CountAsync(x => x.ComponentId == componentId && x.IsRecommended == true);
            var positiveGlobal = await _globalReviewRepo.CountAsync(x => x.ComponentId == componentId && x.IsRecommended == true);

            var avgLocal = localCount / (double)positiveLocal;
            var avgGlobal = globalCount / (double)positiveGlobal;

            return new ReviewsMetricResponse(avgLocal, avgGlobal, localCount, globalCount);
        }
    }
}
