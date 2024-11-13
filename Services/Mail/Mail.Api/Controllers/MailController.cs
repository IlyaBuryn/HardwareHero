using Mail.BusinessLogic.Services;
using Mail.DTOs.Mail;
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
        public async Task<IActionResult> SendMessageAsync([FromBody] MailMessageDto message)
        {
            var response = await _mailService.SendMailAsync(message);

            return Ok(response);
        }

        [HttpPost("send-welcome-message")]
        [AllowAnonymous]
        public async Task<IActionResult> SendWelcomeMessageAsync([FromBody] MailMessageDto message)
        {
            var response = await _mailServicePresets.SendMailAsync(message, MailPresets.Welcome);

            return Ok(response);
        }
    }
}
