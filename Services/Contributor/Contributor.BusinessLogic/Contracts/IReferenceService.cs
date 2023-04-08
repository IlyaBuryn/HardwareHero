namespace Contributor.BusinessLogic.Contracts
{
    public interface IReferenceService
    {
        Task<Guid> AddReferencesAsync(ReferenceDto referenceToAdd);
        Task<bool> UpdateReferencesAsync(ReferenceDto referenceToUpdate);
    }
}
