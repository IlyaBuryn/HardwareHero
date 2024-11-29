using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Chat;
using Contributor.DTOs.Domain.Contributors;
using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Extensions.Repository;

namespace Contributor.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IQueryRepositoryAsync<ChatRoom> _chatRoomRepo;
        private readonly IQueryRepositoryAsync<ContributorModel> _contributorRepo;
        private readonly IQueryRepositoryAsync<ChatMessage> _chatMessageRepo;

        private readonly IMapper _mapper;

        public ChatService(
            IQueryRepositoryAsync<ChatRoom> chatRoomRepo,
            IQueryRepositoryAsync<ContributorModel> contributorRepo,
            IQueryRepositoryAsync<ChatMessage> chatMessageRepo,
            IMapper mapper)
        {
            _chatRoomRepo = chatRoomRepo;
            _contributorRepo = contributorRepo;
            _chatMessageRepo = chatMessageRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> CreateChatRoomAsync(ChatRoomDto chatToAdd)
        {
            chatToAdd.Id = Guid.NewGuid();

            if (!await AllTheseContributorsExist(chatToAdd.Participants))
            {
                throw new NotFoundException("One or more users not found!");
            }
            
            var chat = _mapper.Map<ChatRoom>(chatToAdd);
            var result = await _chatRoomRepo.CreateEntityAsync(chat);
            result.DataAnswerCheck();
            
            return result.Value!.Id;
        }


        public async Task<bool> UpdateChatRoomAsync(ChatRoomDto chatToUpdate)
        {
            if (!await AllTheseContributorsExist(chatToUpdate.Participants))
            {
                throw new NotFoundException("One or more users not found!");
            }

            var chat = await _chatRoomRepo
                .NotFoundCheckAsync(x => x.Id == chatToUpdate.Id);

            chat.Subject = chatToUpdate.Subject;
            chat.Contributors = _mapper.Map<ICollection<ContributorModel>>(chatToUpdate.Participants);
            
            var result = await _chatRoomRepo.UpdateEntityAsync(chat);
            result.DataAnswerCheck();
            
            return result.Value != null;
        }


        public async Task<bool> DeleteChatRoomAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo
                .NotFoundCheckAsync(x => x.Id == chatRoomId);

            var result = await _chatRoomRepo.RemoveEntityAsync(chatRoomId);
            result.DataAnswerCheck();
            
            return result.Value != null;
        }


        public async Task<ChatRoomDto?> GetChatByIdAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo
                .FindAsync(x => x.Id == chatRoomId);
            chat.DataAnswerCheck();

            var result = _mapper.Map<ChatRoomDto>(chat.Value);
            
            return result;
        }


        public async Task<PageResponse<ChatRoomDto?>?> GetChatsByContributorIdAsync(Guid contributorId, IPaginable filter)
        {
            var chats = await _chatRoomRepo
                .FindPagedAsync(x => x.Contributors.Any(x => x.Id == contributorId), filter);
            chats.DataAnswerCheck();

            var page = chats.ToPageResponse();
            var result = _mapper.Map<PageResponse<ChatRoomDto>>(page);

            return result;
        }


        public async Task<Guid?> SendMessageAsync(ChatMessageDto messageToSend)
        {
            var chatRoom = await _chatRoomRepo.NotFoundCheckAsync(x => x.Id == messageToSend.ChatRoomId);
            await _contributorRepo.NotFoundCheckAsync(x => x.Id == messageToSend.SenderId);

            var message = _mapper.Map<ChatMessage>(messageToSend);
            message.Id = Guid.NewGuid();
            chatRoom.ChatMessages.Add(message);

            var result = await _chatRoomRepo.UpdateEntityAsync(chatRoom);
            result.DataAnswerCheck();
            
            return result.Value!.Id;
        }


        public async Task<bool> UpdateMessageAsync(ChatMessageDto messageToSend)
        {
            var chatRoom = await _chatRoomRepo.NotFoundCheckAsync(x => x.Id == messageToSend.ChatRoomId);
            await _contributorRepo.NotFoundCheckAsync(x => x.Id == messageToSend.SenderId);
            var message = await _chatMessageRepo.NotFoundCheckAsync(x => x.Id == messageToSend.Id);

            message.Text = messageToSend.Text;
            message.Timestamp = DateTime.Now;
            message.IsEdited = true;

            var result = await _chatMessageRepo.UpdateEntityAsync(message);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<PageResponse<ChatMessageDto?>?> GetMessagesByChatIdAsync(Guid chatRoomId, IPaginable filter)
        {
            var messages = await _chatMessageRepo
                .FindPagedAsync(x => x.ChatRoomId == chatRoomId, filter);
            messages.DataAnswerCheck();
            var page = messages.ToPageResponse();

            var mappedResult = _mapper.Map<PageResponse<ChatMessageDto>>(page);

            return mappedResult;
        }


        private async Task<bool> AllTheseContributorsExist(ICollection<ContributorModelDto>? contributors)
        {
            foreach (var contributor in contributors)
            {
                var _ = await _contributorRepo.FindEntityAsync(x => x.Id == contributor.Id);
                if (_ == null)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
