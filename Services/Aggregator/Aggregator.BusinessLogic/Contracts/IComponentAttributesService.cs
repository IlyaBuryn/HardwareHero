namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentAttributesService
    {
        Task<Guid?> AddComponentAttributeAsync(ComponentAttributesDto attributeToAdd);
        Task<bool> UpdateComponentAttributeValueAsync(ComponentAttributesDto attributeToUpdate);
        Task<bool> RemoveComponentAttributeAsync(Guid componentId, string attributeKey);

        Task<List<Guid>> ReplaceComponentAttributesAsync(Guid componentId, Dictionary<string, string> attributesToAdd);

        Task<PageResponse<ComponentAttributesDto?>> GetAllUniqueComponentAttributesAsPageAsync(ComponentAttributesFilter filter);
    }
}
