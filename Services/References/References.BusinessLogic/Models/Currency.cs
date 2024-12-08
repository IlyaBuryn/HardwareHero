using HardwareHero.Shared.Models;

namespace References.BusinessLogic.Models
{
    public class Currency : BaseEntity
    {
        public Currency() { }

        public Currency(Guid id, string? code, string? symbol)
        {
            Id = id;
            Code = code;
            Symbol = symbol;
        }

        public string? Code { get; set; }
        public string? Symbol { get; set; }
    }
}
