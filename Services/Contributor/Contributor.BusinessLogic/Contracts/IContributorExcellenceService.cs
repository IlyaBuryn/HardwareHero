using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorExcellenceService
    {
        Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate);
        Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name);
        Task<List<string>> GetExcellenceNamesAsync();
    }
}
