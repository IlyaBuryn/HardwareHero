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

            CreateMap<MaintenanceReferences, MaintenanceReferencesDto>()
                .ReverseMap();
        }
    }
}
