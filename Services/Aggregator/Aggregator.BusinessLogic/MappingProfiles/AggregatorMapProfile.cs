using Aggregator.DataAccess.Models.Components;
using Aggregator.DataAccess.Models.Specifications;
using Aggregator.DTOs.Components;
using Aggregator.DTOs.Specifications;

namespace Aggregator.BusinessLogic.MappingProfiles
{
    public class AggregatorMapProfile : Profile
    {
        public AggregatorMapProfile()
        {
            CreateMap<ComponentLocalReview, ComponentLocalReviewDto>()
                .ReverseMap();

            CreateMap<ComponentGlobalReview, ComponentGlobalReviewDto>()
                .ReverseMap();

            CreateMap<Component, ComponentDto>()
                .ForMember(dto => dto.ComponentType, opt => opt.MapFrom(ent => ent.ComponentType))
                .ForMember(dto => dto.ComponentMetricId, opt => opt.MapFrom(ent => ent.ComponentMetric))
                .ReverseMap();

            CreateMap<ComponentAttribute, ComponentAttributeDto>()
                .ReverseMap();

            CreateMap<ComponentImage, ComponentImageDto>()
                .ReverseMap();

            CreateMap<ComponentType, ComponentTypeDto>()
                .ReverseMap();

            CreateMap<SpecificationFilterType, SpecificationFilterTypeDto>()
                .ReverseMap();

            CreateMap<SpecificationCategory, SpecificationCategoryDto>()
                .ReverseMap();

            CreateMap<SpecificationAttribute, SpecificationAttributeDto>()
                .ReverseMap();
        }
    }
}
