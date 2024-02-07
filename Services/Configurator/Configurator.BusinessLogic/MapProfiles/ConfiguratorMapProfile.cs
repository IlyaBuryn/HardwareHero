namespace Configurator.BusinessLogic.MapProfiles
{
    public class ConfiguratorMapProfile : Profile
    {
        public ConfiguratorMapProfile()
        {
            CreateMap<CustomAssembly, CustomAssemblyDto>()
                .ReverseMap();

            CreateMap<ComponentTypeSigns, ComponentTypeSignsDto>()
                .ForMember(dto => dto.Specifications, opt => opt.MapFrom(ent =>
                ent.Specifications))
                .ReverseMap();

            CreateMap<ComponentTypeSpecification, ComponentTypeSpecificationDto>()
                .ReverseMap();
        }
    }
}
