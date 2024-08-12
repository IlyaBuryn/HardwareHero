using EventStream.EventHandling;
using KafkaEventStream.BackgroundServices;
using KafkaEventStream.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aggregator.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/aggregator")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;
        private readonly PageSizeOptions _pageSizeSettings;
        private readonly ILogger<ComponentController> _logger;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;

        public ComponentController(
            IComponentService componentService,
            IOptions<PageSizeOptions> pageSizeSettings,
            ILogger<ComponentController> logger,
            IMessageProducer messageProducer,
            IMessageConsumer messageConsumer)
        {
            _componentService = componentService;
            _pageSizeSettings = pageSizeSettings.Value;
            _logger = logger;
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
        }

        [HttpPost("component")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateAsync([FromBody] ComponentDto componentToAdd)
        {
            var response = await _componentService
                .AddComponentAsync(componentToAdd);

            return CreatedAtAction(nameof(CreateAsync), response);
        }


        [HttpPut("component")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAsync([FromBody] ComponentDto componentToUpdate)
        {
            var response = await _componentService
                .UpdateComponentAsync(componentToUpdate);
            
            return Ok(response);
        }


        [HttpDelete("component/{componentId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid componentId)
        {
            var response = await _componentService
                .RemoveComponentAsync(componentId);
            
            return Ok(response);
        }


        [HttpPost("components")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateManyAsync([FromBody] List<ComponentDto> componentsToAdd)
        {
            var response = await _componentService
                .AddComponentsAsync(componentsToAdd);

            return Ok(response);
        }


        [HttpGet("component/{componentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid componentId)
        {
            var response = await _componentService
                .GetComponentByIdAsync(componentId);
            
            return Ok(response);
        }

        [HttpPost("components/ids")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIds([FromBody] List<Guid> componentsIds)
        {
            var response = await _componentService
                .GetComponentsByIdsAsync(componentsIds);

            return Ok(response);
        }


        [HttpPost("components/page")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComponents([FromBody] ComponentsFilter filter)
        {
            var response = await _componentService.GetComponentsAsPageAsync(filter);
            
            return Ok(response);
        }
    }
}
