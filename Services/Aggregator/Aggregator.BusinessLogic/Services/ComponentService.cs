using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentService : IComponentService
    {
        private readonly ICollectionRepositoryAsync<Component> _componentRepo;
        private readonly IValidationRepository<Component> _componentValidationRepo;
        private readonly IValidationRepository<ComponentType> _componentTypeValidationRepo;
        private readonly IIMagesRepositoryAsync<ComponentImages> _imagesRepo;
        private readonly IMapper _mapper;

        public ComponentService(
            ICollectionRepositoryAsync<Component> componentRepo,
            IValidationRepository<Component> componentValidationRepo,
            IValidationRepository<ComponentType> componentTypeValidationRepo,
            IMapper mapper,
            IIMagesRepositoryAsync<ComponentImages> imagesRepo)
        {
            _componentRepo = componentRepo;
            _componentValidationRepo = componentValidationRepo;
            _componentTypeValidationRepo = componentTypeValidationRepo;
            _mapper = mapper;
            _imagesRepo = imagesRepo;
        }

        public async Task<Guid?> AddComponentAsync(ComponentDto componentToAdd)
        {
            componentToAdd.Id = Guid.NewGuid();
            _componentValidationRepo.CheckIsAlreadyExist(x => x.Name == componentToAdd.Name,
                new AlreadyExistException(nameof(componentToAdd), componentToAdd.Name));
            _componentTypeValidationRepo.CheckIsNotFound(x => x.Id == componentToAdd.ComponentTypeId,
                new NotFoundException(nameof(ComponentType)));

            if (!componentToAdd.ComponentImages.IsNullOrEmpty())
            {
                foreach (var image in componentToAdd.ComponentImages)
                {
                    await _imagesRepo.SaveImageAsync(_mapper.Map<ComponentImages>(image), 
                        image.ImageData, ComponentImagesDto.GetFileNameExpression());
                }
            }

            var component = _mapper.Map<Component>(componentToAdd);
            var result = await _componentRepo.CreateEntityAsync(component);

            return result;
        }


        public async Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate)
        {
            _componentValidationRepo.CheckIsNotFound(x => x.Id == componentToUpdate.Id,
                new NotFoundException(nameof(Component)));
            _componentValidationRepo.CheckIsAlreadyExist(
                x => x.Name == componentToUpdate.Name && x.Id != componentToUpdate.Id,
                new AlreadyExistException(nameof(componentToUpdate), componentToUpdate.Name));

            var component = await _componentRepo.GetOneEntityAsync(componentToUpdate.Id,
                new IncludeProperties<Component> { IsAllIncludes = true });

            component.Name = componentToUpdate.Name;
            component.Description = componentToUpdate.Description;
            component.ComponentTypeId = componentToUpdate.ComponentTypeId;

            var result = await _componentRepo.UpdateEntityAsync(component);

            return result;
        }


        public async Task<bool> RemoveComponentAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneEntityAsync(componentId,
                new IncludeProperties<Component> { IsAllIncludes = true });
            if (component == null)
            {
                throw new NotFoundException(nameof(Component));
            }

            if (!component.ComponentImages.IsNullOrEmpty())
            {
                foreach (var image in component.ComponentImages)
                {
                    await _imagesRepo.DeleteImageAsync(image,
                        ComponentImagesDto.GetFileNameExpression());
                }
            }

            var result = await _componentRepo.RemoveEntityAsync(componentId);

            return result;
        }


        public async Task<Guid[]> AddComponentsFromJsonAsync(string jsonData)
        {
            var componentsToAdd = JsonConvert.DeserializeObject<List<ComponentDto>>(jsonData);

            if (componentsToAdd == null || !componentsToAdd.Any())
            {
                return new Guid[0];
            }

            var addedComponentIds = new List<Guid>();

            foreach (var componentToAdd in componentsToAdd)
            {
                var componentId = await AddComponentAsync(componentToAdd);
                if (componentId.HasValue)
                {
                    addedComponentIds.Add(componentId.Value);
                }
            }

            return addedComponentIds.ToArray();
        }


        public async Task<ComponentDto?> GetComponentByIdAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneEntityAsync(componentId,
                new IncludeProperties<Component> { IsAllIncludes = true });

            return _mapper.Map<ComponentDto?>(component);
        }


        public async Task<List<ComponentDto?>> GetComponentsByIdsAsync(Guid[] componentsIds)
        {
            var result = new List<ComponentDto?>();

            foreach (var componentId in componentsIds)
            {
                var component = await GetComponentByIdAsync(componentId);
                result.Add(component);
            }

            return result;
        }


        public async Task<PageResponse<ComponentDto?>> GetComponentsAsPageAsync(AggregatorFilter filter)
        {
            _componentValidationRepo.CheckPaginationOptions(filter.paginationInfo,
                new PageOptionsValidationException());

            var query = await _componentRepo.GetManyEntitiesAsync(new IncludeProperties<Component> { IsAllIncludes = true });

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(x => x.Name.Contains(filter.SearchString) ||
                    x.Description.Contains(filter.SearchString));
            }

            if (!filter.Type.IsNullOrEmpty())
            {
                query = query.Where(x => x.ComponentType.Name == filter.Type ||
                    x.ComponentType.FullName == filter.Type);
            }

            if (!filter.AttributeFilters.IsNullOrEmpty())
            {
                foreach (var attributeFilter in filter.AttributeFilters)
                {
                    query = query.Where(x => x.ComponentAttributes
                        .Any(a => a.AttributeName == attributeFilter.Key &&
                            a.AttributeValue.Contains(attributeFilter.Value)));
                }
            }

            query = query.Select(item => new Component
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ComponentTypeId = item.ComponentTypeId,
                ComponentType = item.ComponentType,
                ComponentImages = item.ComponentImages,
            });

            var aggregatedQuery = query.OrderBy(x => x.Name);

            var result = await _componentRepo.GetMappedPageAsync<ComponentDto>(
                aggregatedQuery, filter.paginationInfo, _mapper);

            return result;
        }
    }
}
