using HardwareHero.Filter.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Prices.DTOs.Requests.PricesRequests;

namespace Prices.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/custom-prices")]
    [Authorize]
    public class ContributorPricesController : ControllerBase
    {
        private readonly IContributorPricesService _contributorPricesService;

        public ContributorPricesController(IContributorPricesService contributorPricesService)
        {
            _contributorPricesService = contributorPricesService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> ChangePriceAsync([FromBody] ChangePriceRequest request)
        {
            var response = await _contributorPricesService
               .ChangePriceAsync(request);

            return CreatedAtAction(nameof(ChangePriceAsync), response);
        }

        [HttpGet("{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPositionsAsync([FromRoute] Guid componentId, [FromQuery] IPaginable filter)
        {
            var response = await _contributorPricesService
                .GetPositionsPagedAsync(componentId, filter);

            return Ok(response);
        }

        [HttpGet("{componentId}/low-latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLowestFromLatestPricesAsync([FromRoute] Guid componentId)
        {
            var response = await _contributorPricesService
                .GetLowestFromLatestPricesAsync(componentId);

            return Ok(response);
        }

        [HttpPut("{componentPriceId}/status")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> ChangeUnsupportedStatusAsync([FromRoute] Guid componentPriceId)
        {
            var response = await _contributorPricesService
                .ChangeUnsupportedStatusAsync(componentPriceId);

            return Ok(response);
        }
    }
}
