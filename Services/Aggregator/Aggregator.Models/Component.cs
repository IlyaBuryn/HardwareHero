using HardwareHero.Services.Shared.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aggregator.Models
{
    public class Component : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public string Specifications { get; set; }
        public decimal InitialPrice { get; set; }


        [NotMapped]
        public Dictionary<string, string> SpecificationDictionary
        {
            get
            {
                var result = new Dictionary<string, string>();
                try
                {
                    result = JsonConvert.DeserializeObject<Dictionary<string, string>>(Specifications);
                }
                catch
                {
                    return null;
                }
                return result;
                
            }
            set => Specifications = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public virtual IList<string> ImageList
        {
            get => Images.Split(',');
            set => Images = string.Join(",", value);
        }
    }
}
