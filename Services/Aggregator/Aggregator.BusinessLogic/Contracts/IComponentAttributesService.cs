using Aggregator.DTOs.Components;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentAttributesService
    {
        Task<Guid?> AddComponentAttributeAsync(ComponentAttributeDto attributeToAdd);
        Task<bool> UpdateComponentAttributeValueAsync(ComponentAttributeDto attributeToUpdate);
        Task<bool> RemoveComponentAttributeAsync(Guid componentAttributeId);

        Task<ComponentSpecsResponse> GetComponentSpecsAsync(Guid componentId);
        // TODO: To specs
        // Task<PageResponse<ComponentAttributeDto?>> GetUniqueComponentAttributesPageAsync(ComponentAttributesFilter filter);
        // Task<List<Guid>> ReplaceComponentAttributesAsync(Guid componentId, Dictionary<string, string> attributesToAdd);
    }
}
