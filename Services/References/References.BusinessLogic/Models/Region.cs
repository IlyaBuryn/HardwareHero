using HardwareHero.Shared.Models;

namespace References.BusinessLogic.Models
{
    public class Region : BaseEntity
    {
        public Region() { }

        public Region(Guid id, string? country, string? code, string? city)
        {
            Id = id;
            Country = country;
            Code = code;
            City = city;
        }

        public string? Country { get; set; }
        public string? Code { get; set; }
        public string? City { get; set; }
    }
}
