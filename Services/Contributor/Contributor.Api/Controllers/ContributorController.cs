using Contributor.BusinessLogic.Contracts;
using Contributor.BusinessLogic.Filters;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/contributor")]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        private readonly PageSizeOptions _pageSizeSettings;

        public ContributorController(
            IContributorService contributorService,
            IOptions<PageSizeOptions> pageSizeSettings)
        {
            _contributorService = contributorService;
            _pageSizeSettings = pageSizeSettings.Value;
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> SignUpAsync([FromBody] ContributorModelDto contributorToAdd)
        {
            var response = await _contributorService
                .SignUpContributorAsync(contributorToAdd);
            
            return CreatedAtAction(nameof(SignUpAsync), response);
        }

        [HttpDelete("{contributorId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .RemoveContributorAsync(contributorId);
            
            return Ok(response);
        }

        [HttpGet("{param}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByParamAsync([FromRoute] string param)
        {
            if (Guid.TryParse(param, out Guid userId))
            {
                var response = await _contributorService.GetContributorByUserIdAsync(userId);
                return Ok(response);
            }
            else
            {
                var response = await _contributorService.GetContributorByExcNameAsync(param);
                return Ok(response);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> GetAsPageAsync([FromBody] ContributorsFilter filter)
        {
            filter.ApplyPageSizeOptions(_pageSizeSettings);

            var response = await _contributorService
                .GetContributorsAsPageAsync(filter);
            
            return Ok(response);
        }

        [HttpGet("{contributorId}/confirm-info")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> GetConfirmInfoAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .GetConfirmInfoByContributorIdAsync(contributorId);

            return Ok(response);
        }

        [HttpPut("{contributorId}/confirm-info")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> ChangeConfirmInfoAsync([FromRoute] Guid contributorId, [FromBody] ContributorConfirmInfoDto info)
        {
            var response = await _contributorService
                .ChangeConfirmInfoForContributorAsync(contributorId, info);

            return Ok(response);
        }
    }
}
