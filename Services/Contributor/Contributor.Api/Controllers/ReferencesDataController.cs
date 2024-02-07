using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/references")]
    [Authorize]
    public class ReferencesDataController : ControllerBase
    {
        private readonly IReferencesDataService _referencesDataService;

        public ReferencesDataController(IReferencesDataService referencesDataService)
        {
            _referencesDataService = referencesDataService;
        }

        [HttpPost("region")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> CreateRegionAsync([FromBody] RegionDto regionToAdd)
        {
            var response = await _referencesDataService
                .AddRegionAsync(regionToAdd);

            return CreatedAtAction(nameof(CreateRegionAsync), response);
        }

        [HttpPut("region")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateRegionAsync([FromBody] RegionDto regionToUpdate)
        {
            var response = await _referencesDataService
                .UpdateRegionAsync(regionToUpdate);

            return Ok(response);
        }

        [HttpGet("region/countries")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var response = await _referencesDataService
                .GetCountriesAsync();

            return Ok(response);
        }

        [HttpGet("region/countries/{city}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountriesAsync([FromRoute] string city)
        {
            var response = await _referencesDataService
                .GetRegionByCityAsync(city);

            return Ok(response);
        }

        [HttpGet("region/{country}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegionsByCountryAsync([FromRoute] string country)
        {
            var response = await _referencesDataService
                .GetRegionsByCountryAsync(country);

            return Ok(response);
        }

        [HttpPost("currency")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateCurrencyAsync([FromBody] CurrencyDto currencyToAdd)
        {
            var response = await _referencesDataService
                .AddCurrencyAsync(currencyToAdd);

            return CreatedAtAction(nameof(CreateCurrencyAsync), response);
        }

        [HttpPut("currency")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateCurrencyAsync([FromBody] CurrencyDto currencyToUpdate)
        {
            var response = await _referencesDataService
                .UpdateCurrencyAsync(currencyToUpdate);

            return Ok(response);
        }

        [HttpGet("currencies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            var response = await _referencesDataService
                .GetCurrenciesAsync();

            return Ok(response);
        }
    }
}
