using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IChatService
    {
        Task<bool> ManageMessageAsync(ChatMessageDto messageToSend);
        Task<Guid?> AddChatRoomAsync(ChatRoomDto chatToAdd);
        Task<bool> UpdateChatRoomAsync(ChatRoomDto chatToUpdate);
        Task<bool> DeleteChatRoomAsync(Guid chatRoomId);
        Task<ChatRoomDto?> GetChatByIdAsync(Guid chatRoomId);
        Task<List<ChatRoomDto?>> GetChatsByContributorIdAsync(Guid contributorId);
    }
}
