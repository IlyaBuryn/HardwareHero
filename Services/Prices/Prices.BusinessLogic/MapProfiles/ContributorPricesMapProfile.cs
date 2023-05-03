using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Prices;
using HardwareHero.Services.Shared.Models.Prices;

namespace Prices.BusinessLogic.MapProfiles
{
    public class ContributorPricesMapProfile : Profile
    {
        public ContributorPricesMapProfile()
        {
            CreateMap<ContributorPrice, ContributorPriceDto>()
                .ReverseMap();
        }
    }
}
