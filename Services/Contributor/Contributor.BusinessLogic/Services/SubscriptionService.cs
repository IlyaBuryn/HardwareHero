using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Subscription;
using HardwareHero.Shared.Extensions.Repository;

namespace Contributor.BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IBaseRepositoryAsync<SubscriptionPlanInfo> _subscriptionPlanInfoRepo;
        private readonly IBaseRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly IBaseRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IMapper _mapper;

        public SubscriptionService(
            IBaseRepositoryAsync<SubscriptionPlanInfo> subscriptionPlanInfoRepo,
            IBaseRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            IBaseRepositoryAsync<ContributorModel> contributorRepo,
            IMapper mapper)
        {
            _subscriptionPlanInfoRepo = subscriptionPlanInfoRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _contributorRepo = contributorRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd)
        {
            var subscriptionPlan = _mapper.Map<SubscriptionPlan>(subscriptionPlanToAdd);
            var result = await _subscriptionPlanRepo.CreateEntityAsync(subscriptionPlan);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }

        public async Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate)
        {
            var subscriptionPlan = await _subscriptionPlanRepo
                .NotFoundCheckAsync(x => x.Id == subscriptionPlanToUpdate.Id);

            // TODO: Why?
            if (!await ThisPlanHaveZeroSubscribers(subscriptionPlan.Id))
            {
                throw new DataValidationException("You can't change subscription plan that has subscribers!");
            }

            subscriptionPlan.Price = subscriptionPlanToUpdate.Price;
            subscriptionPlan.DaysCount = subscriptionPlanToUpdate.DaysCount;
            subscriptionPlan.PriorityLevel = subscriptionPlanToUpdate.PriorityLevel;

            var result = await _subscriptionPlanRepo.UpdateEntityAsync(subscriptionPlan);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId)
        {
            var subscriptionPlan = await _subscriptionPlanRepo
                .NotFoundCheckAsync(x => x.Id == subscriptionPlanId);

            if (!await ThisPlanHaveZeroSubscribers(subscriptionPlan.Id))
            {
                throw new DataValidationException("You can't change subscription plan that has subscribers!");
            }

            var result = await _subscriptionPlanRepo.RemoveEntityAsync(subscriptionPlan.Id);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<IEnumerable<SubscriptionPlanDto?>?> GetPlansByCurrencyAsync(Guid currencyId)
        {
            var plans = await _subscriptionPlanRepo.FindAllEntitiesAsync(
                x => x.CurrencyId == currencyId);

            var result = _mapper.Map<List<SubscriptionPlanDto>>(plans.Value);

            return result;
        }

        public async Task<IEnumerable<SubscriptionPlanDto?>?> GetPlansAsync()
        {
            var plansSet = await _subscriptionPlanRepo.FindAllEntitiesAsync();

            var result = _mapper.Map<List<SubscriptionPlanDto>>(plansSet.Value);

            return result;
        }

        public async Task<Guid?> SubscribeContributorAsync(Guid contributorId, Guid subscriptionPlanId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);

            var plan = await _subscriptionPlanRepo
                .NotFoundCheckAsync(x => x.Id == subscriptionPlanId);

            var planInfo = new SubscriptionPlanInfo()
            {
                Id = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                RenewalDate = DateTime.Now.AddDays(plan.DaysCount),
                SubscriptionPlan = plan,
                PlanId = plan.Id
            };

            contributor.SubscriptionPlanInfo = planInfo;
            contributor.SubscriptionPlanInfoId = planInfo.Id;

            var result = await _contributorRepo.UpdateEntityAsync(contributor);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }

        public async Task<bool> UnsubscribeContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);

            var planInfo = await _subscriptionPlanInfoRepo
                .NotFoundCheckAsync(x => x.Id == contributor.SubscriptionPlanInfo!.Id);

            var result = await _subscriptionPlanInfoRepo.RemoveEntityAsync(planInfo.Id);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        private async Task<bool> ThisPlanHaveZeroSubscribers(Guid subscriptionPlanId)
        {
            var subscribers = await _subscriptionPlanInfoRepo
                .FindAllEntitiesAsync(x => x.PlanId == subscriptionPlanId);

            if (subscribers.Value == null || subscribers.Value.Count() == 0)
            {
                return true;
            }

            return subscribers.Value.Count() == 0;
        }
    }
}
