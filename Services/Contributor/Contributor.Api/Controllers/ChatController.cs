using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/contributors/chat")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ChatRoomDto chatToAdd)
        {
            var response = await _chatService
                .AddChatRoomAsync(chatToAdd);
            
            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessageAsync([FromBody] ChatMessageDto messageToSend)
        {
            var response = await _chatService
                .ManageMessageAsync(messageToSend);
            
            return CreatedAtAction(nameof(SendMessageAsync), response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ChatRoomDto chatToUpdate)
        {
            var response = await _chatService
                .UpdateChatRoomAsync(chatToUpdate);
            
            return Ok(response);
        }

        [HttpDelete("{chatRoomId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .DeleteChatRoomAsync(chatRoomId);
            
            return Ok(response);
        }

        [HttpGet("{chatRoomId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .GetChatByIdAsync(chatRoomId);
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatsByContributorIdAsync([FromBody] Guid contributorId)
        {
            var response = await _chatService
                .GetChatsByContributorIdAsync(contributorId);
            
            return Ok(response);
        }
    }
}
