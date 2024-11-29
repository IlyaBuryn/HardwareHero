using Mail.DTOs.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/mail")]
    [Authorize]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IMailServicePresets _mailServicePresets;

        public MailController(
            IMailService mailService,
            IMailServicePresets mailServicePresets)
        {
            _mailService = mailService;
            _mailServicePresets = mailServicePresets;
        }


        [HttpPost("send")]
        [AllowAnonymous]
        public async Task<IActionResult> SendMessageTemplateAsync([FromBody] SendMailEvent message)
        {
            var response = await _mailServicePresets.SendMailTemplateAsync(message);

            return Ok(response);
        }
    }
}
