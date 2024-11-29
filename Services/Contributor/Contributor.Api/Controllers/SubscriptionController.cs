using Contributor.DTOs.Domain.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/subscription-plan")]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }


        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreatePlanAsync([FromBody] SubscriptionPlanDto subscriptionPlanToAdd)
        {
            var response = await _subscriptionService
                .AddSubscriptionPlanAsync(subscriptionPlanToAdd);
            
            return CreatedAtAction(nameof(CreatePlanAsync), response);
        }


        [HttpPut]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdatePlanAsync([FromBody] SubscriptionPlanDto subscriptionPlanToUpdate)
        {
            var response = await _subscriptionService
                .UpdateSubscriptionPlanAsync(subscriptionPlanToUpdate);
            
            return Ok(response);
        }


        [HttpDelete("{subscriptionPlanId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeletePlanAsync([FromRoute] Guid subscriptionPlanId)
        {
            var response = await _subscriptionService
                .RemoveSubscriptionPlanAsync(subscriptionPlanId);

            return Ok(response);
        }


        [HttpGet]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetPlansAsync()
        {
            var response = await _subscriptionService
                .GetPlansAsync();

            return Ok(response);
        }


        [HttpGet("{currencyId}")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetPlansByCurrencyAsync([FromRoute] Guid currencyId)
        {
            var response = await _subscriptionService
                .GetPlansByCurrencyAsync(currencyId);

            return Ok(response);
        }


        [HttpPost("contributor/{contributorId}/subscribe/{subscriptionPlanId}")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> SubscribeAsync([FromRoute] Guid contributorId, [FromRoute] Guid subscriptionPlanId)
        {
            var response = await _subscriptionService
                .SubscribeContributorAsync(contributorId, subscriptionPlanId);

            return CreatedAtAction(nameof(SubscribeAsync), response);
        }


        [HttpPut("contributor/{contributorId}/unsubscribe")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> UnsubscribeAsync([FromRoute] Guid contributorId)
        {
            var response = await _subscriptionService
                .UnsubscribeContributorAsync(contributorId);
            
            return Ok(response);
        }
    }
}
