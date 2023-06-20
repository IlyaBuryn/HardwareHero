using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Newtonsoft.Json;
using System.Linq.Expressions;

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

        public async Task<List<ComponentDto?>> GetComponentsAsPageAsync(int pageNumber, int pageSize, string specificationFilter, string searchString)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new PageOptionsValidationException();
            }

            var component = new Component { Specifications = specificationFilter };
            var componentsSet = await _componentRepo.GetManyEntitiesAsync();
            var filteredComponents = componentsSet;

            if (!string.IsNullOrEmpty(component.Specifications))
            {
                var filters = JsonConvert.DeserializeObject<Dictionary<string, string>>(component.Specifications);
                var expression = BuildExpression(filters);
                filteredComponents = componentsSet.Where(expression);
                
                if (!string.IsNullOrEmpty(searchString) && filteredComponents.Any())
                {
                    filteredComponents = filteredComponents.Where(x => x.Description.Contains(searchString));
                }

                if (!filteredComponents.Any())
                {
                    return new List<ComponentDto?>();
                }
            }

            var page = await _componentRepo.GetPageAsync(filteredComponents, pageNumber, pageSize);

            return _mapper.Map<List<ComponentDto?>>(page);
        }

        public async Task<int> GetComponentsPageCountAsync(int pageSize, string specificationFilter, string searchString)
        {
            if (pageSize <= 0)
            {
                throw new PageOptionsValidationException();
            }

            var component = new Component { Specifications = specificationFilter };
            var componentsSet = await _componentRepo.GetManyEntitiesAsync();
            var filteredComponents = componentsSet;

            if (!string.IsNullOrEmpty(component.Specifications))
            {
                var filters = JsonConvert.DeserializeObject<Dictionary<string, string>>(component.Specifications);
                var expression = BuildExpression(filters);
                filteredComponents = componentsSet.Where(expression);

                if (!string.IsNullOrEmpty(searchString) && filteredComponents.Any())
                {
                    filteredComponents = filteredComponents.Where(x => x.Description.Contains(searchString));
                }

                if (!filteredComponents.Any())
                {
                    return 0;
                }
            }

            return (int)Math.Ceiling((double)filteredComponents.Count() / pageSize);
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
                throw new AlreadyExistException(nameof(componentToAdd), componentToAdd.Name);
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
                throw new AlreadyExistException(nameof(componentToUpdate), componentToUpdate.Name);
            }

            component.Name = componentToUpdate.Name;
            component.Description = componentToUpdate.Description;
            component.Images = componentToUpdate.Images;
            component.Specifications = componentToUpdate.Specifications;
            component.InitialPrice = componentToUpdate.InitialPrice;

            return await _componentRepo.UpdateEntityAsync(component);
        }

        public async Task<decimal> GetComponentAvgMarkAsync(Guid componentId)
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

        private Expression<Func<Component, bool>> BuildExpression(Dictionary<string, string> filters)
        {
            var parameter = Expression.Parameter(typeof(Component), "c");
            Expression body = Expression.Constant(true); // Default to true

            foreach (var filter in filters)
            {
                var key = filter.Key;
                var value = filter.Value;

                var propertyAccess = Expression.Property(parameter, "Specifications");
                var containsKey = Expression.Call(
                    propertyAccess,
                    "Contains",
                    Type.EmptyTypes,
                    Expression.Constant($"\"{key}\":\"")
                );

                var containsValue = Expression.Call(
                    propertyAccess,
                    "Contains",
                    Type.EmptyTypes,
                    Expression.Constant($"\"{value}\"")
                );

                var condition = Expression.AndAlso(containsKey, containsValue);
                body = Expression.AndAlso(body, condition);
            }

            var lambda = Expression.Lambda<Func<Component, bool>>(body, parameter);
            return lambda;
        }
    }
}
