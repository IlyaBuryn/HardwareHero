﻿using Aggregator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Aggregator;
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
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> CreateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddLocalReviewAsync(reviewToAdd);
            
            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPost("component/review/global")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToAdd)
        {
            var response = await _componentReviewService
                .AddGlobalReviewAsync(reviewToAdd);

            return CreatedAtAction(nameof(CreateLocalReviewAsync), response);
        }


        [HttpPut("component/review/local")]
        [AllowAnonymous]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateLocalReviewAsync([FromBody] ComponentLocalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateLocalReviewAsync(reviewToUpdate);

            return Ok(response);
        }

        [HttpPut("component/review/global")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGlobalReviewAsync([FromBody] ComponentGlobalReviewDto reviewToUpdate)
        {
            var response = await _componentReviewService
                .UpdateGlobalReviewAsync(reviewToUpdate);

            return Ok(response);
        }


        [HttpDelete("component/review/local/{reviewId}")]
        [AllowAnonymous]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteLocalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveLocalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpDelete("component/review/global/{reviewId}")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGlobalReviewAsync([FromRoute] Guid reviewId)
        {
            var response = await _componentReviewService
                .RemoveGlobalReviewAsync(reviewId);

            return Ok(response);
        }


        [HttpPost("components/reviews/json")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFromJsonAsync([FromBody] string jsonData)
        {
            var response = await _componentReviewService
                .AddGlobalReviewsFromJsonAsync(jsonData);

            return Ok(response);
        }

        [HttpPost("component/reviews/local/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocalReviewsAsPageByComponentId(
            [FromBody] PaginationInfo paginationInfo, [FromRoute] Guid componentId)
        {
            if (paginationInfo.PageSize <= 0)
            {
                paginationInfo.PageSize = _pageSizeSettings.PageSize;
            }

            var response = await _componentReviewService
                .GetComponentLocalReviewsAsPageByComponentIdAsync(paginationInfo, componentId);

            return Ok(response);
        }

        [HttpPost("component/reviews/global/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGlobalReviewsAsPageByComponentId(
            [FromBody] PaginationInfo paginationInfo, [FromRoute] Guid componentId)
        {
            if (paginationInfo.PageSize <= 0)
            {
                paginationInfo.PageSize = _pageSizeSettings.PageSize;
            }

            var response = await _componentReviewService
                .GetComponentGlobalReviewsAsPageByComponentIdAsync(paginationInfo, componentId);

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
