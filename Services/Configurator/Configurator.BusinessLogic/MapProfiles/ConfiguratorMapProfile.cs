using AutoMapper;
using Configurator.BusinessLogic.Components;
using HardwareHero.Services.Shared.DTOs.Configurator;
using HardwareHero.Services.Shared.Models.Configurator;

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
