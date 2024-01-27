using Aggregator.BusinessLogic.Contracts;
using Aggregator.BusinessLogic.Filters;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentAttributesService : IComponentAttributesService
    {
        private readonly ICollectionRepositoryAsync<ComponentAttributes> _componentAttributesRepo;

        private readonly IValidationRepository<ComponentAttributes> _componentAttributesValidationRepo;

        private readonly IMapper _mapper;

        public ComponentAttributesService(
            ICollectionRepositoryAsync<ComponentAttributes> componentAttributesRepo,
            IValidationRepository<ComponentAttributes> componentAttributesValidationRepo,
            IMapper mapper)
        {
            _componentAttributesRepo = componentAttributesRepo;
            _componentAttributesValidationRepo = componentAttributesValidationRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddComponentAttributeAsync(ComponentAttributesDto attributeToAdd)
        {
            attributeToAdd.Id = Guid.NewGuid();

            _componentAttributesValidationRepo.CheckIsAlreadyExist(
                x => x.ComponentId == attributeToAdd.ComponentId && x.AttributeName == attributeToAdd.AttributeName,
                new AlreadyExistException<ComponentAttributesDto>($"{attributeToAdd.ComponentId} & {attributeToAdd.AttributeName}"));

            var componentAttributes = _mapper.Map<ComponentAttributes>(attributeToAdd);
            var result = await _componentAttributesRepo.CreateEntityAsync(componentAttributes);

            return result;
        }

        public async Task<List<Guid>> ReplaceComponentAttributesAsync(Guid componentId, Dictionary<string, string> attributesToAdd)
        {
            var result = new List<Guid>();

            var attributesSet = await _componentAttributesRepo.GetManyWithDefaultOrEmptyCheckAsync(
                x => x.ComponentId == componentId);

            var attributesList = await attributesSet.ToListAsync();

            foreach (var pair in attributesList)
            {
                if (pair != null && pair.Id != Guid.Empty)
                {
                    await _componentAttributesRepo.RemoveEntityAsync(pair.Id);
                }
            }

            foreach (var pair in attributesToAdd)
            {
                var id = await _componentAttributesRepo.CreateEntityAsync(new ComponentAttributes()
                {
                    AttributeName = pair.Key,
                    AttributeValue = pair.Value,
                    ComponentId = componentId,
                });
                result.Add(id);
            }

            return result;
        }

        public async Task<bool> UpdateComponentAttributeValueAsync(ComponentAttributesDto attributeToUpdate)
        {
            var componentAttribute = await _componentAttributesRepo.GetOneWithNotFoundCheck(
                x => x.ComponentId == attributeToUpdate.ComponentId &&
                x.AttributeName == attributeToUpdate.AttributeName);

            componentAttribute.AttributeValue = attributeToUpdate.AttributeValue;

            var result = await _componentAttributesRepo.UpdateEntityAsync(componentAttribute);

            return result;
        }


        public async Task<bool> RemoveComponentAttributeAsync(Guid componentId, string attributeKey)
        {
            var componentAttribute = await _componentAttributesRepo.GetOneWithNotFoundCheck(
                x => x.ComponentId == componentId &&
                x.AttributeName == attributeKey);

            var result = await _componentAttributesRepo.RemoveEntityAsync(componentAttribute.Id);

            return result;
        }

        public async Task<PageResponse<ComponentAttributesDto?>> GetAllUniqueComponentAttributesAsPageAsync(ComponentAttributesFilter filter)
        {
            _componentAttributesValidationRepo.CheckPaginationOptions(filter.PaginationInfo);

            var query = await _componentAttributesRepo.GetManyEntitiesAsync(
                new IncludeProperties<ComponentAttributes>(x => x.Component, x => x.Component.ComponentType));

            query = query
                .ApplyFilter(filter)
                .ApplyGroupBy(filter);

            var result = await _componentAttributesRepo.GetMappedPageAsync<ComponentAttributesDto>(
                query, filter.PaginationInfo, _mapper);

            return result;
        }
    }
}
