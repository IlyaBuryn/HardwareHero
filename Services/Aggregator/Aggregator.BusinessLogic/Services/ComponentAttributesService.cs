using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

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
                new AlreadyExistException(nameof(attributeToAdd), $"{attributeToAdd.ComponentId} & {attributeToAdd.AttributeName}"));

            var componentAttributes = _mapper.Map<ComponentAttributes>(attributeToAdd);
            var result = await _componentAttributesRepo.CreateEntityAsync(componentAttributes);

            return result;
        }

        // Not working: An error occurred while saving the entity changes. See the inner exception for details.
        // ---> System.InvalidOperationException: There is already an open DataReader associated with this Connection which must be closed first.
        public async Task<List<Guid>> ReplaceComponentAttributesAsync(Guid componentId, Dictionary<string, string> attributesToAdd)
        {
            var result = new List<Guid>();
            var attributes = await _componentAttributesRepo.GetManyEntitiesAsync(
                x => x.ComponentId == componentId);

            foreach (var pair in attributes)
            {
                await _componentAttributesRepo.RemoveEntityAsync(pair.Id);
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
            var componentAttribute = await _componentAttributesRepo.GetOneEntityAsync(
                x => x.ComponentId == attributeToUpdate.ComponentId &&
                x.AttributeName == attributeToUpdate.AttributeName);

            if (componentAttribute == null)
            {
                throw new NotFoundException(nameof(ComponentAttributes));
            }

            componentAttribute.AttributeValue = attributeToUpdate.AttributeValue;

            var result = await _componentAttributesRepo.UpdateEntityAsync(componentAttribute);

            return result;
        }


        public async Task<bool> RemoveComponentAttributeAsync(Guid componentId, string attributeKey)
        {
            var componentAttribute = await _componentAttributesRepo.GetOneEntityAsync(
                x => x.ComponentId == componentId &&
                x.AttributeName == attributeKey);

            if (componentAttribute == null)
            {
                throw new NotFoundException(nameof(ComponentAttributes));
            }

            var result = await _componentAttributesRepo.RemoveEntityAsync(componentAttribute.Id);

            return result;
        }

        // Not Working:"The source 'IQueryable' doesn't implement 'IAsyncEnumerable<HardwareHero.Services.Shared.Models.Aggregator.ComponentAttributes>'. Only sources that implement 'IAsyncEnumerable' can be used for Entity Framework asynchronous operations."
        public async Task<PageResponse<ComponentAttributesDto?>> GetAllUniqueComponentAttributesAsPageAsync(AggregatorFilter aggregatorFilter)
        {
            _componentAttributesValidationRepo.CheckPaginationOptions(aggregatorFilter.paginationInfo,
                new PageOptionsValidationException());

            var attributes = await _componentAttributesRepo.GetManyEntitiesAsync(
                new IncludeProperties<ComponentAttributes> {
                    IncludeExpressions = new Expression<Func<ComponentAttributes, object>>[]
                    {
                        x => x.Component,
                        x => x.Component.ComponentType
                    }
                });

            if (!aggregatorFilter.Type.IsNullOrEmpty())
            {
                attributes = attributes.Where(x => x.Component.ComponentType.Name == aggregatorFilter.Type);
            }

            var attributesGroup = _componentAttributesRepo.GetGroupedBySet(attributes, x => x.AttributeName);

            var result = await _componentAttributesRepo.GetMappedPageAsync<ComponentAttributesDto>(
                attributesGroup, aggregatorFilter.paginationInfo, _mapper);

            return result;
        }
    }
}
