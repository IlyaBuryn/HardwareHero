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
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;

        public ContributorController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromBody] ContributorDto contributorToAdd)
        {
            var response = await _contributorService
                .AddContributorAsync(contributorToAdd);
            
            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ContributorDto contributorToUpdate)
        {
            var response = await _contributorService
                .UpdateContributorAsync(contributorToUpdate);
            
            return Ok(response);
        }

        [HttpDelete("{contributorId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .RemoveContributorAsync(contributorId);
            
            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
        {
            var response = await _contributorService
                .GetContributorByNameAsync(name);
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _contributorService
                .GetContributorsAsync();
            
            return Ok(response);
        }

        [HttpGet("{contributorId}/review-ref")]
        public async Task<IActionResult> GetReviewReferencesByContributorIdAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .GetReviewReferencesByContributorIdAsync(contributorId);
            
            return Ok(response);
        }

        [HttpGet("{contributorId}/component-ref")]
        public async Task<IActionResult> GetComponentReferencesByContributorIdAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .GetComponentReferencesByContributorIdAsync(contributorId);
            
            return Ok(response);
        }

        [HttpGet("{contributorId}/excellence")]
        public async Task<IActionResult> GetExcellenceByContributorIdAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .GetExcellenceByContributorIdAsync(contributorId);
            
            return Ok(response);
        }
    }
}
