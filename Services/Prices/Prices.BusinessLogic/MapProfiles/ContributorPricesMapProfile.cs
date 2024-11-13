using Prices.BusinessLogic.Models;
using Prices.DTOs.Prices;

namespace Prices.BusinessLogic.MapProfiles
{
    public class ContributorPricesMapProfile : Profile
    {
        public ContributorPricesMapProfile()
        {
            CreateMap<ContributorComponentPrices, ContributorComponentPricesDto>()
                .ReverseMap();

            CreateMap<ComponentReferences, ComponentReferencesDto>()
                .ReverseMap();
        }
    }
}
