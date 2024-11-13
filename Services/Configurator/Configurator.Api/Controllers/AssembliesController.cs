using Configurator.DTOs.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configurator.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/assembly")]
    public class AssembliesController : ControllerBase
    {
        private readonly IAssemblyService _assemblyService;

        public AssembliesController(IAssemblyService assemblyService)
        {
            _assemblyService = assemblyService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> SaveAsync(StoredAssemblyDto assemblyToAdd)
        {
            var response = await _assemblyService
                .SaveAssemblyAsync(assemblyToAdd);

            return CreatedAtAction(nameof(SaveAsync), response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(StoredAssemblyDto assemblyToUpdate)
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

        [HttpGet("assemblies/{userId}")]
        public async Task<IActionResult> GetManyByUserIdAsync([FromRoute] Guid userId)
        {
            var response = await _assemblyService
                .GetAssembliesByUserIdAsync(userId);

            return Ok(response);
        }
    }
}
