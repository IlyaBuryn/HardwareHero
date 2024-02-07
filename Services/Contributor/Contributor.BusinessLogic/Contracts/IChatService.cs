namespace Contributor.BusinessLogic.Contracts
{
    public interface IChatService
    {
        Task<Guid?> CreateChatRoomAsync(ChatRoomDto chatRoomToAdd);
        Task<bool> UpdateChatRoomAsync(ChatRoomDto chatRoomToUpdate);
        Task<bool> DeleteChatRoomAsync(Guid chatRoomId);
        Task<ChatRoomDto?> GetChatByIdAsync(Guid chatRoomId);
        Task<PageResponse<ChatRoomDto?>?> GetChatsByContributorIdAsync(Guid contributorId, PaginationInfo paginationInfo);

        Task<Guid?> SendMessageAsync(ChatMessageDto messageToSend);
        Task<bool> UpdateMessageAsync(ChatMessageDto messageToSend);

        Task<PageResponse<ChatMessageDto?>?> GetMessagesByChatIdAsync(Guid chatRoomId, PaginationInfo paginationInfo);

        

    }
}
