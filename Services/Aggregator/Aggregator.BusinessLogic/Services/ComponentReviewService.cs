using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentReviewService : IComponentReviewService
    {
        private readonly IPageRepositoryAsync<ComponentReview> _componentReviewRepo;
        private readonly IMapper _mapper;

        public ComponentReviewService(
            IPageRepositoryAsync<ComponentReview> componentReviewRepo,
            IMapper mapper)
        {
            _componentReviewRepo = componentReviewRepo;
            _mapper = mapper;
        }

        public async Task<Guid> AddComponentReviewAsync(ComponentReviewDto componentReviewToAdd)
        {
            var review = _mapper.Map<ComponentReview>(componentReviewToAdd);
            var result = await _componentReviewRepo.CreateEntityAsync(review);
            
            return result;
        }

        public async Task<List<ComponentReviewDto?>> GetComponentReviewsAsPageByComponentIdAsync(int pageNumber, int pageSize, Guid componentId)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new DataValidationException("Incorrect page number and(or) size provided!");
            }

            var reviews = await _componentReviewRepo.GetManyEntitiesAsync(
                expression: x => x.ComponentId == componentId);
            if (reviews == null || reviews.Count() == 0)
            {
                return new List<ComponentReviewDto?>();
            }

            var page = await _componentReviewRepo.GetPageAsync(reviews, pageNumber, pageSize);

            return _mapper.Map<List<ComponentReviewDto?>>(page);
        }
    }
}
