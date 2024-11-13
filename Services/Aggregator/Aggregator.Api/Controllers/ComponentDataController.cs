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
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateTypeAsync([FromBody] ComponentTypeDto componentTypeToAdd)
        {
            var response = await _componentTypeService
                .AddComponentTypeAsync(componentTypeToAdd);

            return CreatedAtAction(nameof(CreateTypeAsync), response);
        }


        [HttpPut("component/type")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateTypeAsync([FromBody] ComponentTypeDto componentTypeToUpdate)
        {
            var response = await _componentTypeService
                .UpdateComponentTypeAsync(componentTypeToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/type/{typeId}")]
        [Authorize(Roles = Roles.Manager)]
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
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> AddImageAsync([FromBody] ComponentImageDto componentImageToAdd)
        {
            var response = await _componentImagesService
                .AddComponentImageAsync(componentImageToAdd);

            return CreatedAtAction(nameof(AddImageAsync), response);
        }


        [HttpDelete("component/image/{imageId}/{acceptUnrevoked}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteImageAsync([FromRoute] Guid imageId, [FromRoute] bool acceptUnrevoked = false)
        {
            var response = await _componentImagesService
                .RemoveComponentImageAsync(imageId);

            return Ok(response);
        }

        [HttpPut("component/image/{imageId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeActiveImageStatusAsync([FromRoute] Guid imageId)
        {
            var response = await _componentImagesService
                .ChangeActiveImageStatusAsync(imageId);

            return Ok(response);
        }


        [HttpPost("component/attribute")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateAttributeAsync([FromBody] ComponentAttributeDto attributeToAdd)
        {
            var response = await _componentAttributesService
                .AddComponentAttributeAsync(attributeToAdd);

            return CreatedAtAction(nameof(CreateAttributeAsync), response);
        }


        [HttpPut("component/attribute")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAttributeValueAsync(
            [FromBody] ComponentAttributeDto attributeToUpdate)
        {
            var response = await _componentAttributesService
                .UpdateComponentAttributeValueAsync(attributeToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/{componentId}/attribute")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAttributeAsync(
            [FromRoute] Guid componentId, [FromRoute] string key)
        {
            var response = await _componentAttributesService
                .RemoveComponentAttributeAsync(componentId);

            return Ok(response);
        }


        [HttpPost("component/{componentId}/specs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentSpecificationsAsync([FromRoute] Guid componentId)
        {
            var response = await _componentAttributesService
                .GetComponentSpecsAsync(componentId);

            return Ok(response);
        }
    }
}
