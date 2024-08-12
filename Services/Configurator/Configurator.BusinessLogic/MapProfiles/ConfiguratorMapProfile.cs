namespace Configurator.BusinessLogic.MapProfiles
{
    public class ConfiguratorMapProfile : Profile
    {
        public ConfiguratorMapProfile()
        {
            CreateMap<StoredAssembly, StoredAssemblyDto>()
                .ForMember(dto => dto.SelectedComponents, opt => opt.MapFrom(ent =>
                ent.SelectedComponents))
                .ReverseMap();
            
            CreateMap<ConfiguratorComponent, ConfiguratorComponentDto>()
                .ForMember(dto => dto.Attributes, opt => opt.MapFrom(ent =>
                ent.Attributes))
                .ReverseMap();

            CreateMap<ConfiguratorRule, ConfiguratorRuleDto>()
                .ReverseMap();
            
            CreateMap<ConfiguratorComponentAttribute, ConfiguratorComponentAttributeDto>()
                .ReverseMap();
        }
    }
}
