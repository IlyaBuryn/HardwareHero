using HardwareHero.Shared.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HardwareHero.Shared.Data.Configurations
{
    // TODO: ?
    internal class WishListComponentsConfiguration : IEntityTypeConfiguration<WishListComponents>
    {
        public void Configure(EntityTypeBuilder<WishListComponents> builder)
        {
            builder.ToTable("WishListComponents");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
