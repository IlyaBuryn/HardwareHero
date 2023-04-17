using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IPageRepositoryAsync<Component> _componentRepo;
        private readonly IPageRepositoryAsync<ComponentReview> _componentReviewRepo;
        private readonly IMapper _mapper;

        public ComponentService(
            IPageRepositoryAsync<Component> componentRepo,
            IPageRepositoryAsync<ComponentReview> componentReviewRepo,
            IMapper mapper)
        {
            _componentRepo = componentRepo;
            _componentReviewRepo = componentReviewRepo;
            _mapper = mapper;
        }

        public async Task<List<ComponentDto?>> GetComponentsAsPageAsync(int pageNumber, int pageSize, string specificationFilter)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new DataValidationException("Incorrect page number and(or) size provided!");
            }

            var component = new Component { Specifications = specificationFilter };
            var componentsSet = await _componentRepo.GetManyEntitiesAsync();
            var filteredComponents = componentsSet;

            if (component.SpecificationDictionary != null &&
                component.SpecificationDictionary.Count != 0)
            {
                foreach (var filter in component.SpecificationDictionary)
                {
                    filteredComponents = componentsSet
                        .Where(c =>
                            c.Specifications.Contains(filter.Key) ||
                            c.Specifications.Contains(filter.Value));
                    //filteredComponents = filteredComponents
                    //    .Where(c => c.SpecificationDictionary
                    //    .ContainsKey(filter.Key) && c.SpecificationDictionary[filter.Key].Contains(filter.Value));
                }

                if (!filteredComponents.Any())
                {
                    return new List<ComponentDto?>();
                }
            }

            var page = await _componentRepo.GetPageAsync(filteredComponents, pageNumber, pageSize);

            return _mapper.Map<List<ComponentDto?>>(page);
        }

        public async Task<ComponentDto?> GetComponentByIdAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Id == componentId);

            return _mapper.Map<ComponentDto?>(component);
        }

        public async Task<Guid?> AddComponentAsync(ComponentDto componentToAdd)
        {
            var componentName = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Name == componentToAdd.Name);
            if (componentName != null)
            {
                throw new DataValidationException($"This component \"{componentToAdd.Name}\" is already exist!");
            }

            var component = _mapper.Map<Component>(componentToAdd);
            var result = await _componentRepo.CreateEntityAsync(component);
            
            return result;
        }

        public async Task<bool> RemoveComponentAsync(Guid componentId)
        {
            var component = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Id == componentId);
            if (component == null)
            {
                throw new NotFoundException(nameof(component));
            }

            return await _componentRepo.RemoveEntityAsync(componentId);
        }

        public async Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate)
        {
            var component = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Id == componentToUpdate.Id);
            if (component == null)
            {
                throw new NotFoundException(nameof(component));
            }

            var componentName = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Name == componentToUpdate.Name);
            if (componentName != null)
            {
                throw new DataValidationException($"This component \"{componentToUpdate.Name}\" is already exist!");
            }

            component.Name = componentToUpdate.Name;
            component.Description = componentToUpdate.Description;
            component.Images = componentToUpdate.Images;
            component.Specifications = componentToUpdate.Specifications;
            component.InitialPrice = componentToUpdate.InitialPrice;

            return await _componentRepo.UpdateEntityAsync(component);
        }

        public async Task<decimal> GetComponentAvgMark(Guid componentId)
        {
            var component = await _componentRepo.GetOneEntityAsync(
                expression: x => x.Id == componentId);
            if (component == null)
            {
                throw new NotFoundException(nameof(component));
            }

            var reviews = await _componentReviewRepo.GetManyEntitiesAsync(
                expression: x => x.ComponentId == componentId);
            if (reviews == null || reviews.Count() == 0)
            {
                return 0;
            }

            int count = reviews.Count();
            int trueCount = reviews.Count(x => x.Recommended);
            
            return (decimal)trueCount / count * 100;
        }
    }
}
