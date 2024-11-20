using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IBaseRepositoryAsync<ConfiguratorComponent> _componentsRepo;
        private readonly IMapper _mapper;

        public AttributeService(
            IBaseRepositoryAsync<ConfiguratorComponent> componentsRepo,
            IMapper mapper)
        {
            _componentsRepo = componentsRepo;
            _mapper = mapper;
        }

        public List<AttributeGroupDto> GetAttributeGroups(string type)
        {
            // TODO: its should be already in aggregator...
            throw new NotImplementedException();
            //var result = new Dictionary<string, AttributeGroupDto>();

            //var filter = Builders<ConfiguratorComponent>.Filter.Eq(c => c.ComponentType, type);
            //using (var cursor = _componentsCollection.Find(filter).ToCursor())
            //{
            //    while (cursor.MoveNext())
            //    {
            //        var batch = cursor.Current;

            //        foreach (var component in batch)
            //        {
            //            foreach (var attribute in component.Attributes)
            //            {
            //                if (!result.ContainsKey(attribute.Key))
            //                {
            //                    result[attribute.Key] = new AttributeGroupDto
            //                    {
            //                        Key = attribute.Key,
            //                        Values = new List<string>(),
            //                        DataType = "List"
            //                    };
            //                }

            //                var attributeGroup = result[attribute.Key];

            //                var values = attribute.Value.Split(new[] { " / " }, StringSplitOptions.RemoveEmptyEntries);

            //                foreach (var value in values)
            //                {
            //                    if (!attributeGroup.Values.Contains(value))
            //                    {
            //                        attributeGroup.Values.Add(value);
            //                    }
            //                }

            //                if (attribute.Operation == "max" && attributeGroup.DataType != "Range")
            //                {
            //                    attributeGroup.DataType = "Range";
            //                }
            //            }
            //        }
            //    }
            //}

            //return result.Values.ToList();
        }
    }
}
