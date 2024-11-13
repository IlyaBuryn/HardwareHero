using Aggregator.DTOs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/aggregator/specs")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private readonly ISpecificationService _specificationService;
        private readonly ILogger<SpecificationController> _logger;

        public SpecificationController(
            ISpecificationService specificationService,
            ILogger<SpecificationController> logger)
        {
            _specificationService = specificationService;
            _logger = logger;
        }

        [HttpPost("category")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] SpecificationCategoryDto category)
        {
            var response = await _specificationService.AddSpecCategoryAsync(category);

            return CreatedAtAction(nameof(CreateCategoryAsync), response);
        }

        [HttpPut("category")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] SpecificationCategoryDto category)
        {
            var response = await _specificationService.UpdateSpecCategoryAsync(category);

            return Ok(response);
        }

        [HttpDelete("category/{categoryId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid categoryId)
        {
            var response = await _specificationService.RemoveSpecCategoryAsync(categoryId);

            return Ok(response);
        }

        [HttpPost("attribute")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateAttributeAsync([FromBody] SpecificationAttributeDto attribute)
        {
            var response = await _specificationService.AddSpecAttributeAsync(attribute);

            return CreatedAtAction(nameof(CreateAttributeAsync), response);
        }

        [HttpPut("attribute")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAttributeAsync([FromBody] SpecificationAttributeDto attribute)
        {
            var response = await _specificationService.UpdateSpecAttributeAsync(attribute);

            return Ok(response);
        }

        [HttpDelete("attribute/{attributeId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAttributeAsynmc([FromRoute] Guid attributeId)
        {
            var response = await _specificationService.RemoveSpecAttributeAsync(attributeId);

            return Ok(response);
        }

        [HttpGet("for/{componentTypeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSpecificationsGroupsAsync(Guid componentTypeId)
        {
            var response = await _specificationService.GetSpecGroupsAsync(componentTypeId);

            return Ok(response);
        }
    }
}
