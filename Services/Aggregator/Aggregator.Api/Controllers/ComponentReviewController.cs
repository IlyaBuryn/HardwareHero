using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/aggregator")]
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


        [HttpPost("component/review/local")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> CreateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddLocalReviewAsync(reviewToAdd);
            
            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPost("component/review/global")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddGlobalReviewAsync(reviewToAdd);

            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPut("component/review/local")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UpdateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateLocalReviewAsync(reviewToUpdate);

            return Ok(response);
        }

        [HttpPut("component/review/global")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateGlobalReviewAsync(reviewToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/review/local/{reviewId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DeleteLocalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveLocalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpDelete("component/review/global/{reviewId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteGlobalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveGlobalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpPost("components/reviews")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateFromJsonAsync([FromBody] List<ComponentGlobalReviewDto> reviews)
        {
            var response = await _componentReviewService
                .AddGlobalReviewsAsync(reviews);

            return Ok(response);
        }

        [HttpPost("component/{componentId}/reviews/local")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocalReviewsAsPageByComponentId(
            [FromBody] ComponentLocalReviewFilter filter, [FromRoute] Guid componentId)
        {
            var response = await _componentReviewService
                .GetComponentLocalReviewsPageAsync(filter, componentId);

            return Ok(response);
        }

        [HttpPost("component/{componentId}/reviews/global")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGlobalReviewsAsPageByComponentId(
            [FromBody] ComponentGlobalReviewFilter filter, [FromRoute] Guid componentId)
        {
            var response = await _componentReviewService
                .GetComponentGlobalReviewsPageAsync(filter, componentId);

            return Ok(response);
        }


        [HttpGet("component/reviews/metric/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentReviewsMetric([FromRoute] Guid componentId)
        {
            var response = await _componentReviewService
                .GetReviewsMetricForComponent(componentId);
            
            return Ok(response);
        }
    }
}
