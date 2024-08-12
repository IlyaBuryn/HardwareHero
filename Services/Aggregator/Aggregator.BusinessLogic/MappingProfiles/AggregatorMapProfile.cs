namespace Aggregator.BusinessLogic.MappingProfiles
{
    public class AggregatorMapProfile : Profile
    {
        public AggregatorMapProfile()
        {
            CreateMap<ComponentLocalReview, ComponentLocalReviewDto>()
                .ForMember(dto => dto.Component, opt => opt.MapFrom(ent => ent.Component))
                .ReverseMap();

            CreateMap<ComponentGlobalReview, ComponentGlobalReviewDto>()
                .ForMember(dto => dto.Component, opt => opt.MapFrom(ent => ent.Component))
                .ReverseMap();

            CreateMap<Component, ComponentDto>()
                .ForMember(dto => dto.ComponentType, opt => opt.MapFrom(ent => ent.ComponentType))
                .ReverseMap();

            CreateMap<ComponentAttributes, ComponentAttributesDto>()
                .ForMember(dto => dto.Component, opt => opt.MapFrom(ent => ent.Component))
                .ReverseMap();

            CreateMap<ComponentImages, ComponentImagesDto>()
                .ForMember(dto => dto.Component, opt => opt.MapFrom(ent => ent.Component))
                .ReverseMap();

            CreateMap<ComponentType, ComponentTypeDto>().ReverseMap();


            CreateMap<Maintenance, MaintenanceDto>()
                .ForMember(dto => dto.MaintenanceType, opt => opt.MapFrom(ent => ent.MaintenanceType))
                .ReverseMap();

            CreateMap<MaintenanceLocalReview, MaintenanceLocalReviewDto>()
                .ForMember(dto => dto.Maintenance, opt => opt.MapFrom(ent => ent.Maintenance))
                .ReverseMap();

            CreateMap<MaintenanceGlobalReview, MaintenanceGlobalReviewDto>()
                .ForMember(dto => dto.Maintenance, opt => opt.MapFrom(ent => ent.Maintenance))
                .ReverseMap();

            CreateMap<MaintenanceType, MaintenanceTypeDto>().ReverseMap();
        }
    }
}
