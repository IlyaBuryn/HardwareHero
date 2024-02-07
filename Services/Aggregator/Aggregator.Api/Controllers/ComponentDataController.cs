using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/aggregator")]
    public class ComponentDataController : ControllerBase
    {
        private readonly IComponentTypeService _componentTypeService;
        private readonly IComponentImagesService _componentImagesService;
        private readonly IComponentAttributesService _componentAttributesService;
        private readonly PageSizeOptions _pageSizeSettings;

        public ComponentDataController(
            IComponentTypeService componentTypeService,
            IComponentImagesService componentImagesService,
            IComponentAttributesService componentAttributesService,
            IOptions<PageSizeOptions> pageSizeSettings)
        {
            _componentTypeService = componentTypeService;
            _componentImagesService = componentImagesService;
            _componentAttributesService = componentAttributesService;
            _pageSizeSettings = pageSizeSettings.Value;
        }

        [HttpPost("component/type")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateTypeAsync([FromBody] ComponentTypeDto componentTypeToAdd)
        {
            var response = await _componentTypeService
                .AddComponentTypeAsync(componentTypeToAdd);

            return CreatedAtAction(nameof(CreateTypeAsync), response);
        }


        [HttpPut("component/type")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateTypeAsync([FromBody] ComponentTypeDto componentTypeToUpdate)
        {
            var response = await _componentTypeService
                .UpdateComponentTypeAsync(componentTypeToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/type/{typeId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteTypeAsync([FromRoute] Guid typeId)
        {
            var response = await _componentTypeService
                .RemoveComponentTypeAsync(typeId);

            return Ok(response);
        }


        [HttpGet("component/types")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypesAsync()
        {
            var response = await _componentTypeService
                .GetComponentTypesAsync();

            return Ok(response);
        }


        [HttpPost("component/image")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> AddImageAsync([FromBody] ComponentImagesDto componentImageToAdd)
        {
            var response = await _componentImagesService
                .AddComponentImageAsync(componentImageToAdd);

            return CreatedAtAction(nameof(AddImageAsync), response);
        }


        [HttpDelete("component/image/{imageId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteImageAsync([FromRoute] Guid imageId)
        {
            var response = await _componentImagesService
                .RemoveComponentImageAsync(imageId);

            return Ok(response);
        }


        [HttpPost("component/attribute")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateAttributeAsync([FromBody] ComponentAttributesDto attributeToAdd)
        {
            var response = await _componentAttributesService
                .AddComponentAttributeAsync(attributeToAdd);

            return CreatedAtAction(nameof(CreateAttributeAsync), response);
        }


        [HttpPut("component/{componentId}/attributes")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)] 
        public async Task<IActionResult> ReplaceAttributesAsync(
            [FromRoute] Guid componentId, [FromBody] Dictionary<string, string> attributes)
        {
            var response = await _componentAttributesService
                .ReplaceComponentAttributesAsync(componentId, attributes);

            return Ok(response);
        }


        [HttpPut("component/attribute")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAttributeValueAsync(
            [FromBody] ComponentAttributesDto attributeToUpdate)
        {
            var response = await _componentAttributesService
                .UpdateComponentAttributeValueAsync(attributeToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/{componentId}/attribute/{key}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAttributeAsync(
            [FromRoute] Guid componentId, [FromRoute] string key)
        {
            var response = await _componentAttributesService
                .RemoveComponentAttributeAsync(componentId, key);

            return Ok(response);
        }


        [HttpPost("attributes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAttributesGroupAsPageAsync([FromBody] ComponentAttributesFilter filter)
        {
            filter.ApplyPageSizeOptions(_pageSizeSettings);

            var response = await _componentAttributesService
                .GetAllUniqueComponentAttributesAsPageAsync(filter);

            return Ok(response);
        }
    }
}
