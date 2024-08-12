using EventStream.EventHandling;
using HardwareHero.Shared.DTOs.Aggregator;
using KafkaEventStream;
using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/contributor")]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;
        private readonly PageSizeOptions _pageSizeSettings;

        public ContributorController(
            IContributorService contributorService, 
            IMessageProducer messageProducer,
            IMessageConsumer messageConsumer,
            IOptions<PageSizeOptions> pageSizeSettings)
        {
            _contributorService = contributorService;
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
            _pageSizeSettings = pageSizeSettings.Value;
        }

        [HttpPost("sign-up")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> SignUpAsync([FromBody] ContributorModelDto contributorToAdd)
        {
            var response = await _contributorService
                .SignUpContributorAsync(contributorToAdd);
            
            return CreatedAtAction(nameof(SignUpAsync), response);
        }

        [HttpDelete("{contributorId}")]
        [Authorize(Roles = Roles.User)]
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
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> GetAsPageAsync([FromBody] ContributorsFilter filter)
        {
            var response = await _contributorService
                .GetContributorsAsPageAsync(filter);
            
            return Ok(response);
        }

        [HttpGet("{contributorId}/confirm-info")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> GetConfirmInfoAsync([FromRoute] Guid contributorId)
        {
            var response = await _contributorService
                .GetConfirmInfoByContributorIdAsync(contributorId);

            return Ok(response);
        }

        [HttpPut("{contributorId}/confirm-info")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> ChangeConfirmInfoAsync([FromRoute] Guid contributorId, [FromBody] ContributorConfirmInfoDto info)
        {
            var response = await _contributorService
                .ChangeConfirmInfoForContributorAsync(contributorId, info);

            return Ok(response);
        }
    }
}
