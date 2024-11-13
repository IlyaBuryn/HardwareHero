using Configurator.DTOs.Domain;

namespace Configurator.BusinessLogic.Contracts
{
    public interface IAttributeService
    {
        List<AttributeGroupDto> GetAttributeGroups(string type);
    }
}
