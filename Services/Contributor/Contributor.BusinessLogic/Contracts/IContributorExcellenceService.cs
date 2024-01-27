using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorExcellenceService
    {
        Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId);
        Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate);
        Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name);
    }
}
