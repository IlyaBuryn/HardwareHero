using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/contributor")]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        public record ImageDto(string FileName, IFormFile Image);

        public ContributorController(IContributorService contributorService)
        {
            _contributorService = contributorService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync([FromBody] ContributorDto contributorToAdd)
        {
            var response = await _contributorService
                .AddContributorAsync(contributorToAdd);
            
            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPut]
        [Authorize(Roles = "Contributor")]
        public async Task<IActionResult> UpdateAsync([FromBody] ContributorDto contributorToUpdate)
        {
            var response = await _contributorService
                .UpdateContributorAsync(contributorToUpdate);
            
            return Ok(response);
        }

        [HttpDelete("{contributorId}")]
        [Authorize(Roles = "User")]
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

        [HttpGet("by-user/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetContributorByUserIdAsync([FromRoute] Guid userId)
        {
            var response = await _contributorService
                .GetContributorByUserIdAsync(userId);

            return Ok(response);
        }

        [HttpPost("upload-image")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null && file.Length > 0)
                {
                    string filePath = Path.Combine("uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
