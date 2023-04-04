using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.MappingProfiles
{
    public class AggregatorMapProfile : Profile
    {
        public AggregatorMapProfile()
        {
            CreateMap<Component, ComponentDto>().ReverseMap();
            CreateMap<ComponentReview, ComponentReviewDto>()
                .ForMember(dto => dto.Component,  opt => opt.MapFrom(ent => ent.Component))
                .ReverseMap();
        }
    }
}
