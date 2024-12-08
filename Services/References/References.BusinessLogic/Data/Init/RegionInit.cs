using HardwareHero.Shared.Extensions.MongoDb;
using References.BusinessLogic.Models;

namespace References.BusinessLogic.Data.Init
{
    public class RegionInit : MongoDbBaseInit<Region>
    {
        public override IEnumerable<Region> SeedingDatabase()
        {
            return new List<Region>()
            {
                new Region(new Guid("8a29a4f7-164b-4106-b534-dc5e9209f346"), "Belarus", "BY", null),
                new Region(new Guid("aa3d6fc2-6129-4101-90ed-d49c0de13a67"), "Russia", "RU", null),
                new Region(new Guid("e1d77644-f691-4d44-a902-9065e60e82d1"), "Poland", "PL", null),
                new Region(new Guid("1d1e0818-1bff-41a0-b4a4-c39e71b6f5e3"), "France", "FR", null),
                new Region(new Guid("87454aca-2ca3-46ed-9fe5-23e1b4655d55"), "Germany", "DE", null),
                new Region(new Guid("bd01d71b-26ca-487b-ac1b-fdd29fd8cabd"), "United States", "US", null),
            };
        }
    }
}