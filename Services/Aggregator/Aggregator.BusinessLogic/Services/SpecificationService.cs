using Aggregator.DataAccess.Models.Specifications;
using Aggregator.DTOs.Specifications;
using HardwareHero.Shared.Extensions.Repository;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Services
{
    public class SpecificationService : ISpecificationService
    {
        private readonly IBaseRepositoryAsync<SpecificationAttribute> _attributeRepo;
        private readonly IBaseRepositoryAsync<SpecificationCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public SpecificationService(
            IBaseRepositoryAsync<SpecificationAttribute> attributeRepo,
            IBaseRepositoryAsync<SpecificationCategory> categoryRepo,
            IMapper mapper)
        {
            _attributeRepo = attributeRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> AddSpecificationKeyAsync(SpecificationAttributeDto attributeToAdd)
        {
            attributeToAdd.Id = Guid.NewGuid();

            await _attributeRepo.AlreadyExistCheckAsync(x => x.Key == attributeToAdd.Key 
                && x.ComponentTypeId == attributeToAdd.ComponentTypeId);

            var attribute = _mapper.Map<SpecificationAttribute>(attributeToAdd);
            var result = await _attributeRepo.CreateEntityAsync(attribute);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }


        public async Task<Guid?> AddSpecificationCategoryAsync(SpecificationCategoryDto categoryToAdd)
        {
            categoryToAdd.Id = Guid.NewGuid();

            await _categoryRepo.AlreadyExistCheckAsync(x => x.Name == categoryToAdd.Name);

            var category = _mapper.Map<SpecificationCategory>(categoryToAdd);
            var result = await _categoryRepo.CreateEntityAsync(category);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }


        public async Task<SpecGroupsResponse> GetSpecificationKeyGroupsAsync(Guid componentTypeId)
        {
            var attributes = await _attributeRepo.FindAllEntitiesAsync(
                x => x.ComponentTypeId == componentTypeId,
                x => x.SpecificationCategory!);
            attributes.DataAnswerCheck();

            var result = attributes.Value!
                .GroupBy(x => x.SpecificationCategory!.Name)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Key).ToList());
            
            attributes = null;

            return new SpecGroupsResponse(result);
        }


        public async Task<bool> RemoveSpecificationKeyAsync(Guid attributeId)
        {
            await _attributeRepo.NotFoundCheckAsync(x => x.Id == attributeId);

            var result = await _attributeRepo.RemoveEntityAsync(attributeId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveSpecificationCategoryAsync(Guid categoryId)
        {
            await _categoryRepo.NotFoundCheckAsync(x => x.Id ==  categoryId);

            var dependAttributes = await _attributeRepo.FindAllEntitiesAsync(
                x => x.SpecificationCategoryId == categoryId);
            dependAttributes.DataAnswerCheck();

            DependDeleteException.ThrowIfConflict(dependAttributes.Value!.Count(),
                nameof(SpecificationAttribute));
            dependAttributes = null;

            var result = await _categoryRepo.RemoveEntityAsync(categoryId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> UpdateSpecificationKeyAsync(SpecificationAttributeDto attributeToUpdate)
        {
            await _attributeRepo.AlreadyExistCheckAsync(
                x => x.ComponentTypeId == attributeToUpdate.ComponentTypeId
                && x.Key == attributeToUpdate.Key);

            var attribute = await _attributeRepo.FindEntityAsync(x => x.Id == attributeToUpdate.Id);
            attribute.DataAnswerCheck();

            var entity = attribute.Value!;
            entity.Unit = attributeToUpdate.Unit;
            entity.ComponentTypeId = attributeToUpdate.ComponentTypeId;
            entity.Key = attributeToUpdate.Key;
            entity.SpecificationCategoryId = attributeToUpdate.SpecificationCategoryId;
            entity.SpecificationFilterTypeId = attributeToUpdate.SpecificationFilterTypeId;

            var result = await _attributeRepo.UpdateEntityAsync(entity);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> UpdateSpecificationCategoryAsync(SpecificationCategoryDto categoryToUpdate)
        {
            await _categoryRepo.AlreadyExistCheckAsync(
                x => x.Name == categoryToUpdate.Name);

            var category = await _categoryRepo.FindEntityAsync(x => x.Id == categoryToUpdate.Id);
            category.DataAnswerCheck();

            var entity = category.Value!;
            entity.Name = categoryToUpdate.Name;
            entity.Priority = categoryToUpdate.Priority;

            var result = await _categoryRepo.UpdateEntityAsync(entity);
            result.DataAnswerCheck();

            return result.Value != null;
        }
    }
}
