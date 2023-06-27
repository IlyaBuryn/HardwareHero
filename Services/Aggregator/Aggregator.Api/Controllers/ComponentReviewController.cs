using Aggregator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/aggregator")]
    [Authorize]
    public class ComponentReviewController : ControllerBase
    {
        private readonly IComponentReviewService _componentReviewService;
        private readonly PageSizeOptions _pageSizeSettings;

        public ComponentReviewController(
            IComponentReviewService componentReviewService,
            IOptions<PageSizeOptions> pageSizeSettings)
        {
            _componentReviewService = componentReviewService;
            _pageSizeSettings = pageSizeSettings.Value;
        }


        [HttpPost("component-review")]
        public async Task<IActionResult> AddComponentReviewAsync([FromBody] ComponentReviewDto componentReviewToAdd)
        {
            var response = await _componentReviewService
                .AddComponentReviewAsync(componentReviewToAdd);
            
            return CreatedAtAction(nameof(AddComponentReviewAsync), response);
        }


        [HttpGet("component-reviews/{componentId}/{pageNumber}")]
        public async Task<IActionResult> GetComponentReviewsByComponentIdAsync(
            [FromRoute] int pageNumber,
            [FromRoute] Guid componentId,
            [FromHeader(Name = "X-Page-Size")] int? pageSize)
        {
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = _pageSizeSettings.PageSize;
            }

            var response = await _componentReviewService
                .GetComponentReviewsAsPageByComponentIdAsync(pageNumber, (int)pageSize, componentId);
            
            return Ok(response);
        }
    }
}
