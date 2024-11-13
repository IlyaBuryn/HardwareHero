using Aggregator.DTOs.Specifications;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface ISpecificationService
    {
        Task<Guid?> AddSpecCategoryAsync(SpecificationCategoryDto categoryToAdd);
        Task<bool> UpdateSpecCategoryAsync(SpecificationCategoryDto categoryToUpdate);
        Task<bool> RemoveSpecCategoryAsync(Guid categoryId);

        Task<Guid?> AddSpecAttributeAsync(SpecificationAttributeDto attributeToAdd);
        Task<bool> UpdateSpecAttributeAsync(SpecificationAttributeDto attributeToUpdate);
        Task<bool> RemoveSpecAttributeAsync(Guid attributeId);

        Task<SpecGroupsResponse> GetSpecGroupsAsync(Guid componentTypeId);

    }
}
