using Aggregator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/aggregator")]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;
        private readonly PageSizeOptions _pageSizeSettings;

        public ComponentController(
            IComponentService componentService,
            IOptions<PageSizeOptions> pageSizeSettings)
        {
            _componentService = componentService;
            _pageSizeSettings = pageSizeSettings.Value;
        }

        [HttpPost("component")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAsync([FromBody] ComponentDto componentToAdd)
        {
            var response = await _componentService
                .AddComponentAsync(componentToAdd);

            return CreatedAtAction(nameof(CreateAsync), response);
        }


        [HttpPut("component")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateAsync([FromBody] ComponentDto componentToUpdate)
        {
            var response = await _componentService
                .UpdateComponentAsync(componentToUpdate);
            
            return Ok(response);
        }


        [HttpDelete("component/{componentId}")]
        [AllowAnonymous]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid componentId)
        {
            var response = await _componentService
                .RemoveComponentAsync(componentId);
            
            return Ok(response);
        }


        [HttpPost("components/json")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFromJsonAsync([FromBody] string jsonData)
        {
            var response = await _componentService
                .AddComponentsFromJsonAsync(jsonData);

            return Ok(response);
        }


        [HttpGet("component/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid componentId)
        {
            var response = await _componentService
                .GetComponentByIdAsync(componentId);
            
            return Ok(response);
        }

        [HttpPost("components/ids")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIds([FromBody] Guid[] componentsIds)
        {
            var response = await _componentService
                .GetComponentsByIdsAsync(componentsIds);

            return Ok(response);
        }


        [HttpPost("components/page")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponents([FromBody] AggregatorFilter filter)
        {
            if (filter.paginationInfo.PageSize <= 0)
            {
                filter.paginationInfo.PageSize = _pageSizeSettings.PageSize;
            }

            var response = await _componentService.GetComponentsAsPageAsync(filter);
            
            return Ok(response);
        }
    }
}
