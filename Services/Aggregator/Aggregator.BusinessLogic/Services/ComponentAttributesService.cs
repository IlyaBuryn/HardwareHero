using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using HardwareHero.Shared.Extensions.Repository;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentAttributesService : IComponentAttributesService
    {
        private readonly IQueryRepositoryAsync<ComponentAttribute> _componentAttributesRepo;
        private readonly IMapper _mapper;

        public ComponentAttributesService(
            IQueryRepositoryAsync<ComponentAttribute> componentAttributesRepo,
            IMapper mapper)
        {
            _componentAttributesRepo = componentAttributesRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> AddComponentAttributeAsync(ComponentAttributeDto attributeToAdd)
        {
            attributeToAdd.Id = Guid.NewGuid();

            await _componentAttributesRepo.NotFoundCheckAsync(
                x => x.SpecificationAttributeId == attributeToAdd.SpecificationAttributeId);

            var componentAttributes = _mapper.Map<ComponentAttribute>(attributeToAdd);
            var result = await _componentAttributesRepo.CreateEntityAsync(componentAttributes);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> UpdateComponentAttributeValueAsync(ComponentAttributeDto attributeToUpdate)
        {
            var componentAttribute = await _componentAttributesRepo.NotFoundCheckAsync(
                x => x.SpecificationAttributeId == attributeToUpdate.SpecificationAttributeId);

            componentAttribute.Value = attributeToUpdate.Value;

            var result = await _componentAttributesRepo.UpdateEntityAsync(componentAttribute);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveComponentAttributeAsync(Guid componentAttributeId)
        {
            var result = await _componentAttributesRepo.RemoveEntityAsync(componentAttributeId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<ComponentSpecsResponse> GetComponentSpecsAsync(Guid componentId)
        {
            var attributes = await _componentAttributesRepo
                .FindAllEntitiesAsync(
                    x => x.Id == componentId 
                    && x.SpecificationAttribute != null
                    && x.SpecificationAttribute.SpecificationCategory != null,
                x => x.SpecificationAttribute!, x => x.SpecificationAttribute!.SpecificationCategory!);
            attributes.DataAnswerCheck();

            var result = attributes.Value!
                .GroupBy(x => x.SpecificationAttribute!.SpecificationCategory!.Name)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(
                        x => x.SpecificationAttribute!.Key,
                        x => x.Value
                    )
                );

            attributes = null;

            return new ComponentSpecsResponse(result);
        }
    }
}
