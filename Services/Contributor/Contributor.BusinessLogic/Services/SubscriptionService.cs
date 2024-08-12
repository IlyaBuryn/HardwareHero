using Microsoft.EntityFrameworkCore;

namespace Contributor.BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ICrudRepositoryAsync<SubscriptionPlanInfo> _subscriptionPlanInfoRepo;
        private readonly ICrudRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly ICrudRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IMapper _mapper;

        public SubscriptionService(
            ICrudRepositoryAsync<SubscriptionPlanInfo> subscriptionPlanInfoRepo,
            ICrudRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            ICrudRepositoryAsync<ContributorModel> contributorRepo,
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

            return result;
        }

        public async Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate)
        {
            var subscriptionPlan = await _subscriptionPlanRepo
                .GetOneWithNotFoundCheck(x => x.Id == subscriptionPlanToUpdate.Id, false);

            if (!await ThisPlanHaveZeroSubscribers(subscriptionPlan.Id))
            {
                throw new DataValidationException("You can't change subscription plan that has subscribers!");
            }

            subscriptionPlan.Price = subscriptionPlanToUpdate.Price;
            subscriptionPlan.DaysCount = subscriptionPlanToUpdate.DaysCount;
            subscriptionPlan.PriorityLevel = subscriptionPlanToUpdate.PriorityLevel;

            var result = await _subscriptionPlanRepo.UpdateEntityAsync(subscriptionPlan);

            return result;
        }

        public async Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId)
        {
            var subscriptionPlan = await _subscriptionPlanRepo
                .GetOneWithNotFoundCheck(x => x.Id == subscriptionPlanId, false);

            if (!await ThisPlanHaveZeroSubscribers(subscriptionPlan.Id))
            {
                throw new DataValidationException("You can't change subscription plan that has subscribers!");
            }

            var result = await _subscriptionPlanRepo.RemoveEntityAsync(subscriptionPlan.Id);

            return result;
        }

        public async Task<IEnumerable<SubscriptionPlanDto?>?> GetSubscriptionPlansAsync()
        {
            var plansSet = await _subscriptionPlanRepo.GetManyEntitiesAsync();

            var plansList = await plansSet.ToListAsync();

            var result = _mapper.Map<List<SubscriptionPlanDto>>(plansList);

            return result;
        }

        public async Task<Guid?> SubscribeContributorAsync(Guid contributorId, Guid subscriptionPlanId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId);

            contributor.SubscriptionPlanInfo.CheckIfObjectAlreadyExist();

            var plan = await _subscriptionPlanRepo
                .GetOneWithNotFoundCheck(x => x.Id == subscriptionPlanId);

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

            return result ? planInfo.Id : Guid.Empty;
        }

        public async Task<bool> UnsubscribeContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId);

            contributor.SubscriptionPlanInfo.CheckIfObjectNotFound();

            var planInfo = await _subscriptionPlanInfoRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributor.SubscriptionPlanInfo!.Id);

            var result = await _subscriptionPlanInfoRepo.RemoveEntityAsync(planInfo.Id);

            return result;
        }

        private async Task<bool> ThisPlanHaveZeroSubscribers(Guid subscriptionPlanId)
        {
            var subscribers = await _subscriptionPlanInfoRepo
                .GetManyEntitiesAsync(x => x.PlanId == subscriptionPlanId);

            if (subscribers == null || subscribers.Count() == 0)
            {
                return true;
            }

            return subscribers.Count() == 0;
        }
    }
}
