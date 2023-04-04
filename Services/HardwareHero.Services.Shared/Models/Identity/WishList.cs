using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareHero.Services.Shared.Models.Identity
{
    [Table("WishLists")]
    public class WishList : BaseEntity
    {
        public virtual ICollection<WishListComponents>? Components { get; set; }
    }
}
