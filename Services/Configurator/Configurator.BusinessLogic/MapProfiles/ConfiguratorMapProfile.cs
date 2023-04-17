using AutoMapper;
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
        }
    }
}
