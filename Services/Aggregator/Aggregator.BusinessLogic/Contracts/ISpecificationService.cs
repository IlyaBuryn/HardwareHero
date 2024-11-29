using Aggregator.DTOs.Specifications;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface ISpecificationService
    {
        Task<Guid?> AddSpecificationCategoryAsync(SpecificationCategoryDto categoryToAdd);
        Task<bool> UpdateSpecificationCategoryAsync(SpecificationCategoryDto categoryToUpdate);
        Task<bool> RemoveSpecificationCategoryAsync(Guid categoryId);

        Task<Guid?> AddSpecificationKeyAsync(SpecificationAttributeDto attributeToAdd);
        Task<bool> UpdateSpecificationKeyAsync(SpecificationAttributeDto attributeToUpdate);
        Task<bool> RemoveSpecificationKeyAsync(Guid attributeId);

        Task<SpecGroupsResponse> GetSpecificationKeyGroupsAsync(Guid componentTypeId);

    }
}
