﻿namespace HardwareHero.Services.Shared.Models.Identity
{
    public class WishList : BaseEntity
    {
        public virtual ICollection<WishListComponents>? Components { get; set; }
    }
}
