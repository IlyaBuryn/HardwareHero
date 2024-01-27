using Aggregator.BusinessLogic.Contracts;
using Aggregator.BusinessLogic.Filters;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Infrastructure;
using HardwareHero.Services.Shared.Infrastructure.Reviews;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Responses;
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
        [AllowAnonymous]
        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> CreateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddLocalReviewAsync(reviewToAdd);
            
            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPost("component/review/global")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddGlobalReviewAsync(reviewToAdd);

            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPut("component/review/local")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UpdateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateLocalReviewAsync(reviewToUpdate);

            return Ok(response);
        }

        [HttpPut("component/review/global")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateGlobalReviewAsync(reviewToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/review/local/{reviewId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DeleteLocalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveLocalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpDelete("component/review/global/{reviewId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteGlobalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveGlobalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpPost("components/reviews")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateFromJsonAsync([FromBody] List<ComponentGlobalReviewDto> reviews)
        {
            var response = await _componentReviewService
                .AddGlobalReviewsAsync(reviews);

            return Ok(response);
        }

        [HttpPost("component/reviews/local/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocalReviewsAsPageByComponentId(
            [FromBody] ComponentLocalReviewFilter filter, [FromRoute] Guid componentId)
        {
            filter.ApplyPageSizeOptions(_pageSizeSettings);

            var response = await _componentReviewService
                .GetComponentLocalReviewsAsPageByComponentIdAsync(filter, componentId);

            return Ok(response);
        }

        [HttpPost("component/reviews/global/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGlobalReviewsAsPageByComponentId(
            [FromBody] ComponentGlobalReviewFilter filter, [FromRoute] Guid componentId)
        {
            filter.ApplyPageSizeOptions(_pageSizeSettings);

            var response = await _componentReviewService
                .GetComponentGlobalReviewsAsPageByComponentIdAsync(filter, componentId);

            return Ok(response);
        }


        [HttpGet("component/reviews/avg/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvgMarksAsync([FromRoute] Guid componentId)
        {
            var response = await _componentReviewService
                .GetComponentAvgMarkAsync(componentId);
            
            return Ok(response);
        }
    }
}
