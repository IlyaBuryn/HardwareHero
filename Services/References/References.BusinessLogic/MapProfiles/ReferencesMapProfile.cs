using AutoMapper;
using References.BusinessLogic.Models;
using References.DTOs.Domain;

namespace References.BusinessLogic.MapProfiles
{
    public class ReferencesMapProfile : Profile
    {
        public ReferencesMapProfile()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap();

            CreateMap<Currency, CurrencyDto>()
                .ReverseMap();
        }
    }
}
