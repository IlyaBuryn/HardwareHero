using Configurator.BusinessLogic.Components;
using Configurator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Configurator;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Configurator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/configurator/component-type-signs")]
    public class ComponentTypeSignsController : ControllerBase
    {
        private readonly IComponentTypesService _componentTypesService;

        public ComponentTypeSignsController(IComponentTypesService componentTypesService)
        {
            _componentTypesService = componentTypesService;
        }

        [HttpPost("configure")]
        public async Task<IActionResult> SeedDatabaseAsync()
        {
            Type baseClass = typeof(ComponentTypeSigns);
            var components = Assembly.GetAssembly(baseClass).GetTypes().Where(type => type.IsSubclassOf(baseClass));

            var listToSeed = new List<ComponentTypeSigns>();

            foreach (var component in components)
            {
                var instance = (ComponentTypeSigns)Activator.CreateInstance(component);
                var seedToAdd = instance.ConfigureSpecificDescription();
                listToSeed.Add(seedToAdd);
            }

            var response = await _componentTypesService.SeedDatabaseAsync(listToSeed);

            return CreatedAtAction(nameof(SeedDatabaseAsync), response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ComponentTypeSignsDto signsToAdd)
        {
            var response = await _componentTypesService
                .AddComponentTypeSignsAsync(signsToAdd);

            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ComponentTypeSignsDto signsToUpdate)
        {
            var response = await _componentTypesService
                .UpdateComponentTypeSignsAsync(signsToUpdate);

            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var response = await _componentTypesService
                .GetComponentTypeSignsByNameAsync(name);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _componentTypesService
                .GetComponentTypeSignsAsync();

            return new JsonResult(response);
        }
    }
}
