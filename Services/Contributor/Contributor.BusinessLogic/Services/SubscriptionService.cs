using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Contributor.BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ICrudRepositoryAsync<SubscriptionInfo> _subscriptionInfoRepo;
        private readonly ICrudRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ICrudRepositoryAsync<SubscriptionInfo> subscriptionInfoRepo,
            ICrudRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            IMapper mapper)
        {
            _subscriptionInfoRepo = subscriptionInfoRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _mapper = mapper;
        }

        public async Task<bool> UpdateSubscriptionInfoAsync(SubscriptionInfoDto subscriptionInfoToUpdate)
        {
            var subscriptionInfo = await _subscriptionInfoRepo.GetOneEntityAsync(
                expression: x => x.Id == subscriptionInfoToUpdate.Id);
            if (subscriptionInfo == null)
            {
                throw new NotFoundException(nameof(subscriptionInfo));
            }

            var subscriptionPlan = await _subscriptionPlanRepo.GetOneEntityAsync(
                expression: x => x.Id == subscriptionInfoToUpdate.Plan.Id);
            if (subscriptionPlan == null)
            {
                throw new NotFoundException(nameof(subscriptionPlan));
            }

            subscriptionInfo.RenewalDate = subscriptionInfoToUpdate.RenewalDate;
            subscriptionInfo.ExpiryDate = subscriptionInfo.RenewalDate.AddDays(subscriptionPlan.DaysCount);
            subscriptionInfo.Plan = subscriptionPlan;

            var result = await _subscriptionInfoRepo.UpdateEntityAsync(subscriptionInfo);
            return result;
        }

        public async Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd)
        {
            var subscriptionPlan = _mapper.Map<SubscriptionPlan>(subscriptionPlanToAdd);
            var result = await _subscriptionPlanRepo.CreateEntityAsync(subscriptionPlan);
            return result;
        }

        public async Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId)
        {
            var subscriptionPlan = await _subscriptionPlanRepo.GetOneEntityAsync(
                expression: x => x.Id == subscriptionPlanId);
            if (subscriptionPlan == null)
            {
                throw new NotFoundException(nameof(subscriptionPlan));
            }

            if (!await ThisPlanHaveZeroSubscribers(subscriptionPlan.Id))
            {
                throw new DataValidationException("You can't change subscription plan that has subscribers!");
            }

            var result = await _subscriptionPlanRepo.RemoveEntityAsync(subscriptionPlan.Id);
            return result;
        }

        public async Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate)
        {
            var subscriptionPlan = await _subscriptionPlanRepo.GetOneEntityAsync(
                expression: x => x.Id == subscriptionPlanToUpdate.Id);
            if (subscriptionPlan == null)
            {
                throw new NotFoundException(nameof(subscriptionPlan));
            }

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

        private async Task<bool> ThisPlanHaveZeroSubscribers(Guid subscriptionPlanId)
        {
            var subscribers = await _subscriptionInfoRepo.GetManyEntitiesAsync(
                expression: x => x.PlanId == subscriptionPlanId);
            var subscribersCount = subscribers.Count();
            return subscribersCount == 0;
        }
    }
}
