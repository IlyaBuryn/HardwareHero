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
