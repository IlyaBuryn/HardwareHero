using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Models.Contributor;

namespace Contributor.BusinessLogic.MappingProfiles
{
    public class ContributorMapProfile : Profile
    {
        public ContributorMapProfile()
        {
            CreateMap<ContributorModel, ContributorDto>()
                .ForMember(dto => dto.ReviewRef, opt => opt.MapFrom(ent => ent.ReviewRef))
                .ForMember(dto => dto.ComponentRef, opt => opt.MapFrom(ent => ent.ComponentRef))
                .ForMember(dto => dto.SubscriptionInfo, opt => opt.MapFrom(ent => ent.SubscriptionInfo))
                .ForMember(dto => dto.ContributorExcellence, opt => opt.MapFrom(ent => ent.ContributorExcellence))
                .ReverseMap();

            CreateMap<ChatRoom, ChatRoomDto>()
                .ForMember(dto => dto.ChatMessages, opt => opt.MapFrom(ent => ent.ChatMessages))
                .ForMember(dto => dto.Contributors, opt => opt.MapFrom(ent => ent.Contributors))
                .ReverseMap();

            CreateMap<ChatMessage, ChatMessageDto>()
                .ForMember(dto => dto.ChatRoom, opt => opt.MapFrom(ent => ent.ChatRoom))
                .ForMember(dto => dto.Sender, opt => opt.MapFrom(ent => ent.Sender))
                .ReverseMap();

            CreateMap<SubscriptionInfo, SubscriptionInfoDto>()
                .ForMember(dto => dto.Plan, opt => opt.MapFrom(ent => ent.Plan))
                .ForMember(dto => dto.Contributor, opt => opt.MapFrom(ent => ent.Contributor))
                .ReverseMap();

            CreateMap<SubscriptionPlan, SubscriptionPlanDto>().ReverseMap();

            CreateMap<ContributorExcellence, ContributorExcellenceDto>().ReverseMap();

            CreateMap<Reference, ReferenceDto>().ReverseMap();

        }
    }
}
