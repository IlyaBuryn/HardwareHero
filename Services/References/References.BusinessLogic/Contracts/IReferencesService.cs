using static References.DTOs.Responses.ReferencesResponseRecords;

namespace References.BusinessLogic.Contracts
{
    public interface IReferencesService
    {
        Task<GeoReferences> GetGeoReferencesAsync();
    }
}
