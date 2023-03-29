using Aggregator.Models;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;

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
