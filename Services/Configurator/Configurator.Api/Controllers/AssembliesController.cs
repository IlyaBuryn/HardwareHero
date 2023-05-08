using Configurator.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Configurator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configurator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/assemblies")]
    [Authorize]
    public class AssembliesController : ControllerBase
    {
        private readonly IAssemblyService _assemblyService;

        public AssembliesController(IAssemblyService assemblyService)
        {
            _assemblyService = assemblyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomAssemblyDto assemblyToAdd)
        {
            var response = await _assemblyService
                .AddAssemblyAsync(assemblyToAdd);

            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CustomAssemblyDto assemblyToUpdate)
        {
            var response = await _assemblyService
                .UpdateAssemblyAsync(assemblyToUpdate);

            return Ok(response);
        }

        [HttpDelete("{assemblyId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid assemblyId)
        {
            var response = await _assemblyService
                .RemoveAssemblyAsync(assemblyId);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _assemblyService
                .GetAssemblyListAsync();

            return Ok(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync([FromRoute] Guid userId, [FromBody] string ccategory)
        {
            var response = await _assemblyService
                .GetAssemblyListByUserIdAsync(userId, ccategory);

            return Ok(response);
        }

        [HttpGet("{assemblyId}/components")]
        public async Task<IActionResult> GetComponentIdsAsync([FromRoute] Guid assemblyId)
        {
            var response = await _assemblyService
                .GetComponentIdsByAssemblyIdAsync(assemblyId);

            return Ok(response);
        }
    }
}
