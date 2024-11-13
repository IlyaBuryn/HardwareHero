using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareHero.Shared.Models
{
    public class BaseEntity
    {
    #if MSSQL
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    #endif
    #if MONGODB
        [BsonId]
    #endif
        public Guid Id { get; set; }
    }
}
