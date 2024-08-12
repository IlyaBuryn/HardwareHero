using HardwareHero.Filter.Operations;

namespace Contributor.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly ICollectionRepositoryAsync<ChatRoom> _chatRoomRepo;
        private readonly ICollectionRepositoryAsync<ContributorModel> _contributorRepo;
        private readonly ICollectionRepositoryAsync<ChatMessage> _chatMessageRepo;

        private readonly IValidationRepository<ChatRoom> _chatRoomValidationRepo;
        private readonly IValidationRepository<ContributorModel> _contributorValidationRepo;

        private readonly IMapper _mapper;

        public ChatService(
            ICollectionRepositoryAsync<ChatRoom> chatRoomRepo,
            ICollectionRepositoryAsync<ContributorModel> contributorRepo,
            ICollectionRepositoryAsync<ChatMessage> chatMessageRepo,
            IValidationRepository<ChatRoom> chatRoomValidationRepo,
            IValidationRepository<ContributorModel> contributorValidationRepo,
            IMapper mapper)
        {
            _chatRoomRepo = chatRoomRepo;
            _contributorRepo = contributorRepo;
            _chatMessageRepo = chatMessageRepo;
            _chatRoomValidationRepo = chatRoomValidationRepo;
            _contributorValidationRepo = contributorValidationRepo;
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
            
            return result;
        }

        public async Task<bool> UpdateChatRoomAsync(ChatRoomDto chatToUpdate)
        {
            if (!await AllTheseContributorsExist(chatToUpdate.Participants))
            {
                throw new NotFoundException("One or more users not found!");
            }

            var chat = await _chatRoomRepo
                .GetOneWithNotFoundCheck(x => x.Id == chatToUpdate.Id);

            chat.Subject = chatToUpdate.Subject;
            chat.Participants = _mapper.Map<ICollection<ContributorModel>>(chatToUpdate.Participants);
            
            var result = await _chatRoomRepo.UpdateEntityAsync(chat);
            
            return result;
        }

        public async Task<bool> DeleteChatRoomAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo
                .GetOneWithNotFoundCheck(x => x.Id == chatRoomId);

            var result = await _chatRoomRepo.RemoveEntityAsync(chatRoomId);
            
            return result;
        }

        public async Task<ChatRoomDto?> GetChatByIdAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo
                .GetManyWithDefaultOrEmptyCheckAsync(x => x.Id == chatRoomId, true);

            var result = _mapper.Map<ChatRoomDto>(chat);
            
            return result;
        }

        public async Task<PageResponse<ChatRoomDto?>?> GetChatsByContributorIdAsync(Guid contributorId, IPaginable filter)
        {
            _chatRoomValidationRepo.CheckPaginationOptions(filter);

            var chats = await _chatRoomRepo
                .GetManyWithDefaultOrEmptyCheckAsync(x => x.Participants.Any(x => x.Id == contributorId));

            var result = await _chatRoomRepo.GetMappedPageAsync(chats, filter);
            var mappedResult = _mapper.Map<PageResponse<ChatRoomDto>>(result);

            return mappedResult;
        }

        public async Task<Guid?> SendMessageAsync(ChatMessageDto messageToSend)
        {
            _chatRoomValidationRepo.CheckIfObjectNotFound(x => x.Id == messageToSend.ChatRoomId);
            _contributorValidationRepo.CheckIfObjectNotFound(x => x.Id == messageToSend.SenderId);

            var chatRoom = await _chatRoomRepo
                .GetOneWithNotFoundCheck(x => x.Id == messageToSend.ChatRoomId);

            var message = _mapper.Map<ChatMessage>(messageToSend);
            message.Id = Guid.NewGuid();
            chatRoom.ChatMessages.Add(message);

            var result = await _chatRoomRepo.UpdateEntityAsync(chatRoom);
            
            return result ? message.Id : Guid.Empty;
        }

        public async Task<bool> UpdateMessageAsync(ChatMessageDto messageToSend)
        {
            _chatRoomValidationRepo.CheckIfObjectNotFound(x => x.Id == messageToSend.ChatRoomId);
            _contributorValidationRepo.CheckIfObjectNotFound(x => x.Id == messageToSend.SenderId);

            var chatRoom = await _chatRoomRepo
                .GetOneWithNotFoundCheck(x => x.Id == messageToSend.ChatRoomId);

            var message = await _chatMessageRepo
                .GetOneWithNotFoundCheck(x => x.Id == messageToSend.Id);

            message.Text = messageToSend.Text;
            message.Timestamp = DateTime.Now;
            message.IsEdited = true;

            var result = await _chatMessageRepo.UpdateEntityAsync(message);

            return result;
        }

        public async Task<PageResponse<ChatMessageDto?>?> GetMessagesByChatIdAsync(Guid chatRoomId, IPaginable filter)
        {
            _chatRoomValidationRepo.CheckPaginationOptions(filter);

            var messages = await _chatMessageRepo
                .GetManyWithDefaultOrEmptyCheckAsync(x => x.ChatRoomId == chatRoomId);

            var result = await _chatMessageRepo.GetMappedPageAsync(messages.Reverse(), filter);
            var mappedResult = _mapper.Map<PageResponse<ChatMessageDto>>(result);

            return mappedResult;
        }

        private async Task<bool> AllTheseContributorsExist(ICollection<ContributorModelDto>? contributors)
        {
            foreach (var contributor in contributors)
            {
                var _ = await _contributorRepo.GetOneEntityAsync(
                    expression: x => x.Id == contributor.Id);
                if (_ == null)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
