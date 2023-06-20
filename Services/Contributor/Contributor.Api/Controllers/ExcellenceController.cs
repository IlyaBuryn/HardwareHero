using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/excellence")]
    [Authorize]
    public class ExcellenceController : ControllerBase
    {
        private readonly IContributorExcellenceService _excellenceService;

        public ExcellenceController(IContributorExcellenceService excellenceService)
        {
            _excellenceService = excellenceService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ContributorExcellenceDto excellenceToUpdate)
        {
            var response = await _excellenceService
                .UpdateExcellenceAsync(excellenceToUpdate);
            
            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var response = await _excellenceService
                .GetExcellenceByNameAsync(name);
            
            return Ok(response);
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetNamesAsync()
        {
            var response = await _excellenceService
                .GetExcellenceNamesAsync();
            
            return Ok(response);
        }
    }
}
