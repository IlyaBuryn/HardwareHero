using HardwareHero.Shared.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Shared.Data.Configurations
{
    public class WishListComponentConfiguration : IEntityTypeConfiguration<WishListComponents>
    {
        public void Configure(EntityTypeBuilder<WishListComponents> builder)
        {
            builder.Property(u => u.ComponentId).IsRequired();
            builder.Property(u => u.ApplicationUserId).IsRequired();
        }
    }
}