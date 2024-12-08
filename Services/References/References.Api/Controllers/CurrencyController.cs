using HardwareHero.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using References.BusinessLogic.Contracts;
using References.DTOs.Domain;

namespace References.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/currency")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(
            ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }


        [HttpGet("every")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            var result = await _currencyService.GetCurrenciesAsync();

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateCurrencyAsync(
            [FromBody] CurrencyDto currencyToCreate)
        {
            var result = await _currencyService.AddCurrencyAsync(
                currencyToCreate);

            return CreatedAtAction(nameof(CreateCurrencyAsync), result);
        }


        [HttpPut]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateCurrencyAsync(
            [FromBody] CurrencyDto currencyToUpdate)
        {
            var result = await _currencyService.UpdateCurrencyAsync(
                currencyToUpdate);

            return Ok(result);
        }
    }
}
