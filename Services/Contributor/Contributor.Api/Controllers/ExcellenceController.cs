using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/contributor")]
    [Authorize]
    public class ExcellenceController : ControllerBase
    {
        private readonly IContributorExcellenceService _excellenceService;

        public ExcellenceController(IContributorExcellenceService excellenceService)
        {
            _excellenceService = excellenceService;
        }

        [HttpGet("{contributorId}/excellence")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] Guid contributorId)
        {
            var response = await _excellenceService
                .GetExcellenceByContributorIdAsync(contributorId);

            return Ok(response);
        }

        [HttpPut("excellence")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> UpdateAsync([FromBody] ContributorExcellenceDto excellenceToUpdate)
        {
            var response = await _excellenceService
                .UpdateExcellenceAsync(excellenceToUpdate);
            
            return Ok(response);
        }

        [HttpGet("excellence/{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var response = await _excellenceService
                .GetExcellenceByNameAsync(name);
            
            return Ok(response);
        }
    }
}
