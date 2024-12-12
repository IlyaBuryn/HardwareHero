using HardwareHero.Shared.Extensions.MongoDb;
using References.BusinessLogic.Models;

namespace References.BusinessLogic.Data.Init
{
    public class CurrencyInit : MongoDbBaseInit<Currency>
    {
        public override IEnumerable<Currency> SeedingDatabase()
        {
            return new List<Currency>()
            {
                new Currency(new Guid("2200add2-b26e-4b78-a2bd-e6721eca0bcb"), "BYN", "Br"),
                new Currency(new Guid("8cb73308-b77d-428b-9cf2-6ff3c1f33709"), "RUB", "₽"),
                new Currency(new Guid("0bb32ff2-4821-4828-9a6d-418c64a4989f"), "PLN", "zł"),
                new Currency(new Guid("93e7717c-04d4-447f-8dfd-4a669cfa1d36"), "USD", "$"),
                new Currency(new Guid("b6d10808-c502-42b9-a07f-51406296f8a3"), "EUR", "€"),
                new Currency(new Guid("a5df38ad-4501-41f5-bd61-52206b25c61c"), "GBP", "£"),
                new Currency(new Guid("1d3b0d48-8ded-48e1-bcd7-508b599e348f"), "JPY", "¥"),
                new Currency(new Guid("9ac95404-243e-409c-94c5-e448edd9aba5"), "CNY", "¥"),
                new Currency(new Guid("2dd3f70a-fa7d-465b-a453-5d0177cc33a6"), "CHF", "₣"),
                new Currency(new Guid("9f16b901-cb8b-4abf-91ba-e9041cf05ce9"), "AUD", "$"),
                new Currency(new Guid("3d847c6e-d13f-4541-815f-ea0a2dda2c39"), "CAD", "$"),
            };
        }
    }
}