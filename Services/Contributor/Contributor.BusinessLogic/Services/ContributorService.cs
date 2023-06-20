using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorService : IContributorService
    {
        private readonly ICrudRepositoryAsync<ContributorModel> _contributorRepo;
        private readonly ICrudRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly ICrudRepositoryAsync<ContributorExcellence> _excellenceRepo;
        private readonly IMapper _mapper;

        public ContributorService(
            ICrudRepositoryAsync<ContributorModel> contributorRepo,
            ICrudRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            ICrudRepositoryAsync<ContributorExcellence> excellenceRepo,
            IMapper mapper)
        {
            _contributorRepo = contributorRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _excellenceRepo = excellenceRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddContributorAsync(ContributorDto contributorToAdd)
        {
            var contributorCheck = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.UserId == contributorToAdd.UserId);
            if (contributorCheck != null)
            {
                throw new AlreadyExistException("user", string.Empty, "contributor");
            }

            var excellenceCheck = await _excellenceRepo.GetOneEntityAsync(
                expression: x => x.Name == contributorToAdd.ContributorExcellence.Name);
            if (excellenceCheck != null)
            {
                throw new AlreadyExistException("company name");
            }

            var contributor = _mapper.Map<ContributorModel>(contributorToAdd);
            var result = await _contributorRepo.CreateEntityAsync(contributor);
            
            return result;
        }

        public async Task<ReferenceDto?> GetComponentReferencesByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.Id == contributorId);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            var result = _mapper.Map<ReferenceDto>(contributor.ComponentRef);
            
            return result;
        }

        public async Task<ReferenceDto?> GetReviewReferencesByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.Id == contributorId);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            var result = _mapper.Map<ReferenceDto>(contributor.ReviewRef);
            
            return result;
        }

        public async Task<List<ContributorDto?>> GetContributorsAsync()
        {
            var contributors = await _contributorRepo.GetManyEntitiesAsync(
                includeProperties: new System.Linq.Expressions.Expression<Func<ContributorModel, object>>[] {
                    x => x.ContributorExcellence,
                    x => x.SubscriptionInfo,
                    x => x.ComponentRef,
                    x => x.ReviewRef });
            var result = _mapper.Map<List<ContributorDto>>(contributors.ToList());
            
            return result;
        }

        public async Task<bool> RemoveContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.Id == contributorId);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }
            
            var result = await _contributorRepo.RemoveEntityAsync(contributorId);
            
            return result;
        }

        public async Task<bool> UpdateContributorAsync(ContributorDto contributorToUpdate)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.Id == contributorToUpdate.Id);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            //var subscriptionPlan = await _subscriptionPlanRepo.GetOneEntityAsync(
            //    expression: x => x.Id == contributorToUpdate.SubscriptionInfo.Plan.Id);
            //if (subscriptionPlan == null)
            //{
            //    throw new NotFoundException(nameof(subscriptionPlan));
            //}

            contributor.IsConfirmed = contributorToUpdate.IsConfirmed;
            contributor.Region = contributorToUpdate.Region;
            contributor.ReviewRef = _mapper.Map<Reference>(contributorToUpdate.ReviewRef);
            contributor.ComponentRef = _mapper.Map<Reference>(contributorToUpdate.ComponentRef);
            contributor.SubscriptionInfo = _mapper.Map<SubscriptionInfo>(contributorToUpdate.SubscriptionInfo);

            var result = await _contributorRepo.UpdateEntityAsync(contributor);
            
            return result;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.Id == contributorId);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            var result = _mapper.Map<ContributorExcellenceDto>(contributor.ContributorExcellence);
            
            return result;
        }

        public async Task<ContributorDto?> GetContributorByNameAsync(string name)
        {
            // TODO: check for functionality;
            var contributor = await _contributorRepo.GetOneEntityAsync(
                includeProperties: x => x.ContributorExcellence,
                expression: x => x.ContributorExcellence.Name == name);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            var result = _mapper.Map<ContributorDto>(contributor);
            
            return result;
        }

        public async Task<ContributorDto?> GetContributorByUserIdAsync(Guid userId)
        {
            var contributor = await _contributorRepo.GetOneEntityAsync(
                expression: x => x.UserId == userId);
            if (contributor == null)
            {
                throw new NotFoundException(nameof(contributor));
            }

            var result = _mapper.Map<ContributorDto>(contributor);

            return result;
        }
    }
}
