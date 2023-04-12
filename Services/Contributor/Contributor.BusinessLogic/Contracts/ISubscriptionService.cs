using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface ISubscriptionService
    {
        Task<Guid?> AddSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToAdd);
        Task<bool> UpdateSubscriptionPlanAsync(SubscriptionPlanDto subscriptionPlanToUpdate);
        Task<bool> RemoveSubscriptionPlanAsync(Guid subscriptionPlanId);
        Task<bool> UpdateSubscriptionInfoAsync(SubscriptionInfoDto subscriptionInfoToUpdate);
    }
}
