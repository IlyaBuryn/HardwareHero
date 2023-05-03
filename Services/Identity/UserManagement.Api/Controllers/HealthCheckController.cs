using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/health-check")]
    [AllowAnonymous]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Check() => "Service is online";
    }
}
