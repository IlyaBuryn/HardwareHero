using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.Comparers;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Settings;
using Microsoft.Extensions.Options;

namespace Contributor.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly ICrudRepositoryAsync<ChatRoom> _chatRoomRepo;
        private readonly ICrudRepositoryAsync<ContributorModel> _contributorRepo;
        private readonly ChatOptions _chatSettings;
        private readonly IMapper _mapper;

        public ChatService(
            ICrudRepositoryAsync<ChatRoom> chatRoomRepo,
            ICrudRepositoryAsync<ContributorModel> contributorRepo,
            IMapper mapper,
            IOptions<ChatOptions> chatSettings)
        {
            _chatRoomRepo = chatRoomRepo;
            _contributorRepo = contributorRepo;
            _mapper = mapper;
            _chatSettings = chatSettings.Value;
        }

        public async Task<Guid?> AddChatRoomAsync(ChatRoomDto chatToAdd)
        {
            if (chatToAdd.Contributors.Count() > _chatSettings.ContributorsInChatMaxCount)
            {
                throw new DataValidationException("Too many chat users!");
            }

            if (!await AllTheseContributorsExist(chatToAdd.Contributors))
            {
                throw new NotFoundException("One or more users not found!");
            }
            
            var chat = _mapper.Map<ChatRoom>(chatToAdd);
            var result = await _chatRoomRepo.CreateEntityAsync(chat);
            
            return result;
        }

        public async Task<bool> UpdateChatRoomAsync(ChatRoomDto chatToUpdate)
        {
            if (chatToUpdate.Contributors.Count() > _chatSettings.ContributorsInChatMaxCount)
            {
                throw new DataValidationException("Too many chat users!");
            }

            if (!await AllTheseContributorsExist(chatToUpdate.Contributors))
            {
                throw new NotFoundException("One or more users not found!");
            }

            var chat = await _chatRoomRepo.GetOneEntityAsync(
                expression: x => x.Id == chatToUpdate.Id);
            if (chat == null)
            {
                throw new NotFoundException(nameof(chat));
            }

            chat.Contributors = _mapper
                .Map<ICollection<ContributorModel>>(chatToUpdate.Contributors);
            var result = await _chatRoomRepo.UpdateEntityAsync(chat);
            
            return result;
        }

        public async Task<bool> DeleteChatRoomAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo.GetOneEntityAsync(
                expression: x => x.Id == chatRoomId);
            if (chat == null)
            {
                throw new NotFoundException(nameof(chat));
            }

            var result = await _chatRoomRepo.RemoveEntityAsync(chatRoomId);
            
            return result;
        }

        public async Task<ChatRoomDto?> GetChatByIdAsync(Guid chatRoomId)
        {
            var chat = await _chatRoomRepo.GetOneEntityAsync(
                expression: x => x.Id == chatRoomId,
                includeProperties: x => x.ChatMessages);
            if (chat == null )
            {
                throw new NotFoundException(nameof(chat));
            }

            var result = _mapper.Map<ChatRoomDto>(chat);
            
            return result;
        }

        public async Task<List<ChatRoomDto?>> GetChatsByContributorIdAsync(Guid contributorId)
        {
            // TODO: check for functionality;
            var chats = await _chatRoomRepo.GetManyEntitiesAsync();
            var result = new List<ChatRoomDto>();

            foreach (var chat in chats)
            {
                if (chat.Contributors.Contains(
                    new ContributorModel() { Id = contributorId },
                    new ContributorModelComparer()))
                {
                    result.Add(_mapper.Map<ChatRoomDto>(chat));
                }
            }

            return result;
        }

        public async Task<bool> ManageMessageAsync(ChatMessageDto messageToSend)
        {
            // TODO: check for functionality;
            var chatRoom = await _chatRoomRepo.GetOneEntityAsync(
                expression: x => x.Id == messageToSend.ChatRoom.Id);
            if (chatRoom == null)
            {
                throw new NotFoundException(nameof(chatRoom));
            }

            var message = _mapper.Map<ChatMessage>(messageToSend);
            chatRoom.ChatMessages.Add(message);

            var result = await _chatRoomRepo.UpdateEntityAsync(chatRoom);
            
            return result;
        }

        private async Task<bool> AllTheseContributorsExist(ICollection<ContributorDto> contributors)
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
