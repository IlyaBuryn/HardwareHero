using Microsoft.AspNetCore.Http;

namespace HardwareHero.Shared.DTOs.Contributor
{
    public class CurrencyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
