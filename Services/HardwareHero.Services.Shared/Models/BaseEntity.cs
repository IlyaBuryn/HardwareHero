using System.ComponentModel.DataAnnotations;

namespace HardwareHero.Services.Shared.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
