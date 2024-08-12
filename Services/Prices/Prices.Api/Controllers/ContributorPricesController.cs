using HardwareHero.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        //[Authorize(Roles = Roles.Contributor)]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePriceAsync([FromBody] ChangePriceRequest request)
        {
            var response = await _contributorPricesService
               .ChangePriceAsync(request);

            return CreatedAtAction(nameof(ChangePriceAsync), response);
        }

        [HttpGet("{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponentPricesAsync([FromRoute] Guid componentId)
        {
            var response = await _contributorPricesService
                .GetComponentPricesAsync(componentId);

            return Ok(response);
        }

        //[HttpPost("")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetPricesToDiscreetlyUpdate([FromBody] PaginationInfo pageInfo)
        //{
        //    var response = await _contributorPricesService
        //        .GetPricesToDiscreetlyUpdate(pageInfo);

        //    return Ok(response);
        //}
    }
}
