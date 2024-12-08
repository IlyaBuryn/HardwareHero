using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using References.BusinessLogic.Contracts;

namespace References.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/references")]
    [Authorize]
    public class ReferencesController : ControllerBase
    {
        private readonly IReferencesService _referencesService;

        public ReferencesController(
            IReferencesService referencesService)
        {
            _referencesService = referencesService;
        }


        [HttpGet("geo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGeoReferencesAsync()
        {
            var result = await _referencesService.GetGeoReferencesAsync();

            return Ok(result);
        }
    }
}
