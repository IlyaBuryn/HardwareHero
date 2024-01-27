﻿using Aggregator.BusinessLogic.Contracts;
using Aggregator.BusinessLogic.Filters;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Microsoft.Extensions.Options;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentService : IComponentService
    {
        private readonly ICollectionRepositoryAsync<Component> _componentRepo;

        private readonly ICrudRepositoryAsync<ComponentViews> _componentViewsRepo;
        private readonly ICrudRepositoryAsync<ComponentType> _componentTypeRepo;

        private readonly IValidationRepository<Component> _componentValidationRepo;
        private readonly IValidationRepository<ComponentType> _componentTypeValidationRepo;

        private readonly IObjectImageRepositoryAsync<ComponentImages> _imagesRepo;

        private readonly IMapper _mapper;

        private readonly string _fileNameDivider;

        public ComponentService(
            ICollectionRepositoryAsync<Component> componentRepo,
            ICrudRepositoryAsync<ComponentViews> componentViewsRepo,
            IValidationRepository<Component> componentValidationRepo,
            IValidationRepository<ComponentType> componentTypeValidationRepo,
            IObjectImageRepositoryAsync<ComponentImages> imagesRepo,
            IMapper mapper,
            IOptions<ImagesSaveOptions> savePathOptions,
            ICrudRepositoryAsync<ComponentType> componentTypeRepo)
        {
            _componentRepo = componentRepo;
            _componentViewsRepo = componentViewsRepo;
            _componentValidationRepo = componentValidationRepo;
            _componentTypeValidationRepo = componentTypeValidationRepo;
            _imagesRepo = imagesRepo;
            _mapper = mapper;
            _fileNameDivider = savePathOptions.Value.FileNameDivider ?? string.Empty;
            _componentTypeRepo = componentTypeRepo;
        }

        public async Task<Guid?> AddComponentAsync(ComponentDto componentToAdd)
        {
            componentToAdd.Id = Guid.NewGuid();

            _componentValidationRepo.CheckIfObjectAlreadyExist(x => x.Name == componentToAdd.Name, componentToAdd.Name);
            _componentTypeValidationRepo.CheckIfObjectNotFound(x => x.Id == componentToAdd.ComponentTypeId);

            if (componentToAdd.ComponentImages != null && componentToAdd.ComponentImages.Count() != 0)
            {
                foreach (var image in componentToAdd.ComponentImages)
                {
                    await _imagesRepo.SaveImageAsync(_mapper.Map<ComponentImages>(image), 
                        image.ImageData, image.ComponentId + _fileNameDivider + image.Image);
                }
            }

            var component = _mapper.Map<Component>(componentToAdd);
            var result = await _componentRepo.CreateEntityAsync(component);

            return result;
        }


        public async Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate)
        {
            _componentValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Name == componentToUpdate.Name && x.Id != componentToUpdate.Id,
                componentToUpdate.Name);

            var component = await _componentRepo
                .GetOneWithNotFoundCheck(x => x.Id == componentToUpdate.Id);

            var componentType = await _componentTypeRepo
                .GetOneWithNotFoundCheck(x => x.Id == componentToUpdate.ComponentTypeId);

            component.Name = componentToUpdate.Name;
            component.Description = componentToUpdate.Description;
            component.ComponentTypeId = componentType.Id;

            var result = await _componentRepo.UpdateEntityAsync(component);

            return result;
        }


        public async Task<bool> RemoveComponentAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneWithNotFoundCheck(x => x.Id == componentId);

            // No need to delete the test data yet, however this code works.
            //if (component.ComponentImages != null && component.ComponentImages.Count() != 0)
            //{
            //    foreach (var image in component.ComponentImages)
            //    {
            //        await _imagesRepo.DeleteImageAsync(image,
            //            image.ComponentId + _fileNameDivider + image.Image);
            //    }
            //}

            var result = await _componentRepo.RemoveEntityAsync(componentId);

            return result;
        }


        public async Task<ComplexResponse> AddComponentsAsync(IEnumerable<ComponentDto> componentsToAdd)
        {
            var result = new ComplexResponse();

            foreach (var componentDto in componentsToAdd)
            {
                try
                {
                    await AddComponentAsync(componentDto);
                    result.Responses.Add(new ComplexResponse.TupleResponse(componentDto.Name, true.ToString()));
                }
                catch (Exception ex) 
                {
                    result.Responses.Add(new ComplexResponse.TupleResponse(componentDto.Name, ex.Message));
                }
            }

            return result;   
        }


        public async Task<ComponentDto?> GetComponentByIdAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneWithNotFoundCheck(x => x.Id == componentId);

            await IncrementComponentView(componentId);

            return _mapper.Map<ComponentDto?>(component);
        }


        public async Task<List<ComponentDto?>> GetComponentsByIdsAsync(List<Guid> componentsIds)
        {
            var result = new List<ComponentDto?>();

            foreach (var componentId in componentsIds)
            {
                var component = await _componentRepo.GetOneWithNotFoundCheck(x => x.Id == componentId);
                result.Add(_mapper.Map<ComponentDto?>(component));
            }

            return result;
        }


        public async Task<PageResponse<ComponentDto?>> GetComponentsAsPageAsync(ComponentsFilter filter)
        {
            _componentValidationRepo.CheckPaginationOptions(filter.PaginationInfo);

            var query = await _componentRepo.GetManyEntitiesAsync(new IncludeProperties<Component>());

            query = query
                .ApplyFilter(filter);

            if (filter.AttributeFilters != null && filter.AttributeFilters.Count() != 0)
            {
                foreach (var attributeFilter in filter.AttributeFilters)
                {
                    query = query.Where(x => x.ComponentAttributes
                        .Any(a => a.AttributeName == attributeFilter.Key &&
                            a.AttributeValue.Contains(attributeFilter.Value)));
                }
            }

            query = query
                .ApplyOrderBy(filter)
                .ApplySelection(filter);

            var result = await _componentRepo.GetMappedPageAsync<ComponentDto>(
                query, filter.PaginationInfo, _mapper);

            return result;
        }

        private async Task IncrementComponentView(Guid componentId)
        {
            var componentView = await _componentViewsRepo.GetOneEntityAsync(x => x.ComponentId == componentId);
            if (componentView == null)
            {
                await _componentViewsRepo.CreateEntityAsync(new ComponentViews
                {
                    ComponentId = componentId,
                    ViewsCount = 1,
                });
            }
            else
            {
                var updatedComponentView = componentView;
                updatedComponentView.ViewsCount += 1;
                await _componentViewsRepo.UpdateEntityAsync(updatedComponentView);
            }
        }
    }
}
