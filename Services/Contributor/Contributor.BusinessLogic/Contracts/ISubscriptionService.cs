namespace Contributor.BusinessLogic.Contracts
{
    public interface ISubscriptionService
    {
        Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd);
        Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate);
        Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId);
        Task<IEnumerable<SubscriptionPlanDto?>?> GetSubscriptionPlansAsync();

        Task<Guid?> SubscribeContributorAsync(Guid contributorId, Guid subscriptionPlanId);
        Task<bool> UnsubscribeContributorAsync(Guid contributorId);
    }
}
