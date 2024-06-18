namespace Prices.BusinessLogic.Contracts
{
    public interface IPricesWorkerService
    {
        Task<bool> DiscreetlyTryToUpdatePriceAsync(Guid contributorComponentPricesId);
    }
}
