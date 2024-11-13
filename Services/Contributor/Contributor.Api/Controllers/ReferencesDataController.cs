using Contributor.DTOs.Domain.Currencies;
using Contributor.DTOs.Domain.Regions;
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
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> CreateRegionAsync([FromBody] RegionDto regionToAdd)
        {
            var response = await _referencesDataService
                .AddRegionAsync(regionToAdd);

            return CreatedAtAction(nameof(CreateRegionAsync), response);
        }

        [HttpPut("region")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateRegionAsync([FromBody] RegionDto regionToUpdate)
        {
            var response = await _referencesDataService
                .UpdateRegionAsync(regionToUpdate);

            return Ok(response);
        }

        [HttpGet("regions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegionsAsync()
        {
            var response = await _referencesDataService
                .GetRegionsAsync();

            return Ok(response);
        }

        [HttpGet("regions/{countryCode}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegionsByCodeAsync([FromRoute] string countryCode)
        {
            var response = await _referencesDataService
                .GetRegionsByCountryAsync(countryCode);

            return Ok(response);
        }

        [HttpGet("regions/{countryCode}/cities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCitiesByCodeAsync([FromRoute] string countryCode)
        {
            var response = await _referencesDataService
                .GetCitiesByCountryAsync(countryCode);

            return Ok(response);
        }

        [HttpGet("region/{city}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountriesAsync([FromRoute] string city)
        {
            var response = await _referencesDataService
                .GetRegionByCityAsync(city);

            return Ok(response);
        }

        [HttpPost("currency")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateCurrencyAsync([FromBody] CurrencyDto currencyToAdd)
        {
            var response = await _referencesDataService
                .AddCurrencyAsync(currencyToAdd);

            return CreatedAtAction(nameof(CreateCurrencyAsync), response);
        }

        [HttpPut("currency")]
        [Authorize(Roles = Roles.Manager)]
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

        [HttpGet("references")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEverythingAsync()
        {
            var response = await _referencesDataService
                .GetCurrenciesAndRegionsAsync();

            return Ok(response);
        }
    }
}
