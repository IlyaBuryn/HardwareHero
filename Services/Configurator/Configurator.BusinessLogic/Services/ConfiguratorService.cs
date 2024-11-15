using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;
using HardwareHero.Shared.Repositories.Contracts;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    // TODO: This service need to rework
    //public class ConfiguratorService : IConfiguratorService
    //{
    //    private readonly IBaseRepositoryAsync<ConfiguratorRule> _rulesRepo;
    //    private readonly IMapper _mapper;


        //public ConfiguratorService(
        //    IBaseRepositoryAsync<ConfiguratorRule> rulesRepo,
        //    IMapper mapper)
        //{
        //    _rulesRepo = rulesRepo;
        //    _mapper = mapper;
        //}

        //public async Task<AssemblyCompatibilityResult> CheckCompatibility(StoredAssembly assembly)
        //{
        //    var result = new AssemblyCompatibilityResult();
        //    var _rules = await _rulesCollection.Find(Builders<ConfiguratorRule>.Filter.Empty).ToListAsync();

        //    foreach (var rule in _rules)
        //    {
        //        var components1 = assembly.SelectedComponents.Where(c => c.ComponentType == rule.ComponentType1);
        //        var components2 = assembly.SelectedComponents.Where(c => c.ComponentType == rule.ComponentType2);

        //        foreach (var component1 in components1)
        //        {
        //            foreach (var component2 in components2)
        //            {
        //                if (CompareAttributes(component1, component2, rule))
        //                {
        //                    continue;
        //                }

        //                result.IsCompatible = false;
        //                result.Problems.Add($"Incompatible with {rule.ComponentType1} and {rule.ComponentType2} at {rule.AttributeName1}/{rule.AttributeName2} params");
        //            }
        //        }
        //    }

        //    return result;
        //}

        //private bool CompareAttributes(ConfiguratorComponent component1, ConfiguratorComponent component2, ConfiguratorRule rule)
        //{
        //    var attribute1 = component1.Attributes.FirstOrDefault(a => a.Key == rule.AttributeName1);
        //    var attribute2 = component2.Attributes.FirstOrDefault(a => a.Key == rule.AttributeName2);

        //    // TODO: idk, this should work
        //    if (attribute1 != null && attribute2 != null)
        //    {
        //        if (attribute1.Value == "value" && attribute2.Value != "value")
        //        {
        //            return attribute2.Compare(attribute1);
        //        }
        //        else if (attribute1.Value == "max" && attribute2.Value == "bool")
        //        {
        //            return attribute2.Compare(attribute1);
        //        }

        //        return attribute1.Compare(attribute2);
        //    }

        //    return true;
        //}
    //}
}
