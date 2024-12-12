using HardwareHero.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace References.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/region")]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(
            IRegionService regionService)
        {
            _regionService = regionService;
        }


        [HttpGet("every")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegionsAsync()
        {
            var result = await _regionService.GetRegionsAsync();

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateRegionAsync(
            [FromBody] RegionDto regionToCreate)
        {
            var result = await _regionService.AddRegionAsync(
                regionToCreate);

            return CreatedAtAction(nameof(CreateRegionAsync), result);
        }


        [HttpPut]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateRegionAsync(
            [FromBody] RegionDto regionToUpdate)
        {
            var result = await _regionService.UpdateRegionAsync(
                regionToUpdate);

            return Ok(result);
        }
    }
}
