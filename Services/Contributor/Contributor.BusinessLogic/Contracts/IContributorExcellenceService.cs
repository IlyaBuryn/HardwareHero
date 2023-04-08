namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorExcellenceService
    {
        Task<Guid?> AddExcellenceAsync(ContributorExcellenceDto excellenceToAdd);
        Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate);
        Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name);
        Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId);
        Task<ContributorDto?> GetContributorByExcellenceIdAsync(Guid excellenceId);
        Task<List<string>> GetExcellenceNamesAsync();
    }
}
