using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Models.Contributor;

namespace Contributor.BusinessLogic.MappingProfiles
{
    public class ContributorMapProfile : Profile
    {
        public ContributorMapProfile()
        {
            CreateMap<ContributorModel, ContributorModelDto>()
                .ForMember(dto => dto.ContributorExcellence, opt => opt.MapFrom(ent => ent.ContributorExcellence))
                .ForMember(dto => dto.ContributorConfirmInfo, opt => opt.MapFrom(ent => ent.ContributorConfirmInfo))
                .ForMember(dto => dto.SubscriptionPlanInfo, opt => opt.MapFrom(ent => ent.SubscriptionPlanInfo))
                .ForMember(dto => dto.ChatRooms, opt => opt.MapFrom(ent => ent.ChatRooms))
                .ReverseMap();

            CreateMap<ChatRoom, ChatRoomDto>()
                .ForMember(dto => dto.ChatMessages, opt => opt.MapFrom(ent => ent.ChatMessages))
                .ForMember(dto => dto.Participants, opt => opt.MapFrom(ent => ent.Participants))
                .ReverseMap();

            CreateMap<ChatMessage, ChatMessageDto>()
                .ForMember(dto => dto.ChatRoom, opt => opt.MapFrom(ent => ent.ChatRoom))
                .ForMember(dto => dto.Sender, opt => opt.MapFrom(ent => ent.Sender))
                .ReverseMap();

            CreateMap<SubscriptionPlanInfo, SubscriptionPlanInfoDto>()
                .ForMember(dto => dto.SubscriptionPlan, opt => opt.MapFrom(ent => ent.SubscriptionPlan))
                .ReverseMap();

            CreateMap<ContributorExcellence, ContributorExcellenceDto>()
                .ForMember(dto => dto.Currency, opt => opt.MapFrom(ent => ent.Currency))
                .ForMember(dto => dto.Region, opt => opt.MapFrom(ent => ent.Region))
                .ReverseMap();

            CreateMap<SubscriptionPlan, SubscriptionPlanDto>().ReverseMap();

            CreateMap<ContributorConfirmInfo, ContributorConfirmInfoDto>().ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<Currency, CurrencyDto>().ReverseMap();
        }
    }
}
