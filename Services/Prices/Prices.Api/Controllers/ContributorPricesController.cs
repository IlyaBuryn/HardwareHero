using HardwareHero.Services.Shared.DTOs.Prices;
using Microsoft.AspNetCore.Mvc;
using Prices.BusinessLogic.Contracts;

namespace Prices.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("custom-prices")]
    public class ContributorPricesController : ControllerBase
    {
        private readonly IContributorPricesService _contributorPricesService;

        public ContributorPricesController(IContributorPricesService contributorPricesService)
        {
            _contributorPricesService = contributorPricesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ContributorPriceDto priceToAdd)
        {
            var response = await _contributorPricesService
               .AddContributorPriceAsync(priceToAdd);

            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpGet("{componentId}")]
        public async Task<IActionResult> GetPricesByComponentIdAsync([FromRoute] Guid componentId)
        {
            var response = await _contributorPricesService
                .GetContributorPricesByComponentIdAsync(componentId);

            return Ok(response);
        }

        [HttpGet("{componentId}/latest")]
        public async Task<IActionResult> GetLatestPriceByComponentIdAsync([FromRoute] Guid componentId)
        {
            var response = await _contributorPricesService
                .GetLastPriceAsync(componentId);

            return Ok(response);
        }
    }
}
