using Aggregator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("aggregator")]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpPost("component")]
        [Authorize("ClientIdPolicy")] //(Roles = "Manager")
        public async Task<IActionResult> AddComponentAsync([FromBody] ComponentDto componentToAdd)
        {
            var response = await _componentService.AddComponentAsync(componentToAdd);
            return CreatedAtAction(nameof(AddComponentAsync), response);
        }


        [HttpPut("component")]
        [Authorize("ClientIdPolicy")] //(Roles = "Manager")
        public async Task<IActionResult> UpdateComponentAsync([FromBody] ComponentDto componentToUpdate)
        {
            var response = await _componentService.UpdateComponentAsync(componentToUpdate);
            return Ok(response);
        }


        [HttpDelete("component/{componentId}")]
        [Authorize("ClientIdPolicy")] //(Roles = "Manager")
        public async Task<IActionResult> RemoveComponentAsync([FromRoute] Guid componentId)
        {
            var response = await _componentService.RemoveComponentAsync(componentId);
            return Ok(response);
        }


        [HttpGet("component/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentById([FromRoute] Guid componentId)
        {
            var response = await _componentService.GetComponentByIdAsync(componentId);
            return Ok(response);
        }


        [HttpGet("component-mark/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentAvgMark([FromRoute] Guid componentId)
        {
            var response = await _componentService.GetComponentAvgMark(componentId);
            return Ok(response);
        }


        [HttpGet("components/{pageNumber}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponents(
            [FromRoute] int pageNumber,
            [FromHeader(Name = "X-Specification-Filter")] string specificationFilter,
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings)
        {
            var response = await _componentService.GetComponentsAsPageAsync(pageNumber, pageSizeSettings.Value.PageSize, specificationFilter);
            return Ok(response);
        }
    }
}
