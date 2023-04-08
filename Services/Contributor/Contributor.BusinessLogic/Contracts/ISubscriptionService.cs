namespace Contributor.BusinessLogic.Contracts
{
    public interface ISubscriptionService
    {
        Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd);
        Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate);
        Task<bool> RemoveSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToRemove);
        //TODO: Maybe I should add new model `SubscriptionInfo`->`SubscriptionPlan`
        //      that contain info about plan (date, discount, ~~~)
    }
}
