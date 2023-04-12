using Contributor.BusinessLogic.Contracts;
using Contributor.BusinessLogic.Services;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("excellence")]
    public class ExcellenceController : ControllerBase
    {
        private readonly IContributorExcellenceService _excellenceService;

        public ExcellenceController(IContributorExcellenceService excellenceService)
        {
            _excellenceService = excellenceService;
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync([FromBody] ContributorExcellenceDto excellenceToUpdate)
        {
            var response = await _excellenceService
                .UpdateExcellenceAsync(excellenceToUpdate);
            return Ok(response);
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var response = await _excellenceService
                .GetExcellenceByNameAsync(name);
            return Ok(response);
        }

        [HttpGet("names")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNamesAsync()
        {
            var response = await _excellenceService
                .GetExcellenceNamesAsync();
            return Ok(response);
        }
    }
}
