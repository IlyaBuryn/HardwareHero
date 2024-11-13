using Contributor.DTOs.Domain.Subscription;

namespace Contributor.BusinessLogic.Contracts
{
    public interface ISubscriptionService
    {
        Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd);
        Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate);
        Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId);
        Task<IEnumerable<SubscriptionPlanDto?>?> GetPlansAsync();
        Task<IEnumerable<SubscriptionPlanDto?>?> GetPlansByCurrencyAsync(Guid currencyId);

        Task<Guid?> SubscribeContributorAsync(Guid contributorId, Guid subscriptionPlanId);
        Task<bool> UnsubscribeContributorAsync(Guid contributorId);
    }
}
