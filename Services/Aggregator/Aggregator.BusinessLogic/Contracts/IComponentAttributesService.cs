using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Responses;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentAttributesService
    {
        Task<Guid?> AddComponentAttributeAsync(ComponentAttributesDto attributeToAdd);
        Task<bool> UpdateComponentAttributeValueAsync(ComponentAttributesDto attributeToUpdate);
        Task<bool> RemoveComponentAttributeAsync(Guid componentId, string attributeKey);

        Task<List<Guid>> ReplaceComponentAttributesAsync(Guid componentId, Dictionary<string, string> attributesToAdd);

        Task<PageResponse<ComponentAttributesDto?>> GetAllUniqueComponentAttributesAsPageAsync(AggregatorFilter aggregatorFilter);
    }
}
