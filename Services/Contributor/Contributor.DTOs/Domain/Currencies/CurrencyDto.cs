namespace Contributor.DTOs.Domain.Currencies
{
    public class CurrencyDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Symbol { get; set; }
    }
}
