using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("plan")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePlanAsync([FromBody] SubscriptionPlanDto subscriptionPlanToAdd)
        {
            var response = await _subscriptionService
                .AddSubscriptionPlanAsync(subscriptionPlanToAdd);
            
            return CreatedAtAction(nameof(CreatePlanAsync), response);
        }

        [HttpPut("plan")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePlanAsync([FromBody] SubscriptionPlanDto subscriptionPlanToUpdate)
        {
            var response = await _subscriptionService
                .UpdateSubscriptionPlanAsync(subscriptionPlanToUpdate);
            
            return Ok(response);
        }

        [HttpPut("info")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateContributorPlanInfoAsync([FromBody] SubscriptionInfoDto subscriptionInfoToUpdate)
        {
            var response = await _subscriptionService
                .UpdateSubscriptionInfoAsync(subscriptionInfoToUpdate);
            
            return Ok(response);
        }

        [HttpDelete("plan/{subscriptionPlanId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePlanAsync([FromRoute] Guid subscriptionPlanId)
        {
            var response = await _subscriptionService
                .RemoveSubscriptionPlanAsync(subscriptionPlanId);
            
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlansAsync()
        {
            var response = await _subscriptionService
                .GetSubscriptionPlansAsync();
            
            return Ok(response);
        }
    }
}
