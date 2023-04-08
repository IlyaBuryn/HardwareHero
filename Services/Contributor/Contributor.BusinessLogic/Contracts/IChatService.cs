namespace Contributor.BusinessLogic.Contracts
{
    public interface IChatService
    {
        Task<Guid?> SendMessageAsync(ChatMessageDto messageToSend);
        Task<bool> UpdateMessageAsync(ChatMessageDto messageToUpdate);
        Task<bool> DeleteMessageAsync(Guid chatMessageId);

        Task<Guid?> AddChatRoomAsync(ChatRoomDto chatToAdd);
        Task<bool> UpdateChatRoomAsync(ChatRoomDto chatToUpdate);
        Task<bool> DeleteChatRoomAsync(Guid chatRoomId);
        Task<ChatRoomDto> GetChatWithMessagesByIdAsync(Guid chatRoomId);
    }
}
