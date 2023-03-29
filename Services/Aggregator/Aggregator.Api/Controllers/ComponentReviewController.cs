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
    public class ComponentReviewController : ControllerBase
    {
        private readonly IComponentReviewService _componentReviewService;

        public ComponentReviewController(IComponentReviewService componentReviewService)
        {
            _componentReviewService = componentReviewService;
        }


        [HttpPost("component-review")]
        [AllowAnonymous]
        public async Task<IActionResult> AddComponentReviewAsync([FromBody] ComponentReviewDto componentReviewToAdd)
        {
            var response = await _componentReviewService.AddComponentReviewAsync(componentReviewToAdd);
            return CreatedAtAction(nameof(AddComponentReviewAsync), response);
        }


        [HttpGet("component-reviews/{componentId}/{pageNumber}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentReviewsByComponentIdAsync(
            [FromRoute] int pageNumber,
            [FromRoute] Guid componentId,
            [FromServices] IOptions<PageSizeSettings> pageSizeSettings)
        {
            var response = await _componentReviewService
                .GetComponentReviewsAsPageByComponentIdAsync(pageNumber, pageSizeSettings.Value.PageSize, componentId);
            return Ok(response);
        }
    }
}
