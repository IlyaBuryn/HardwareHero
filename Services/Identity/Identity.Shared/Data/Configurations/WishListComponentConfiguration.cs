using Identity.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Shared.Data.Configurations
{
    public class WishListComponentConfiguration : IEntityTypeConfiguration<WishListComponent>
    {
        public void Configure(EntityTypeBuilder<WishListComponent> builder)
        {
            builder.Property(u => u.ComponentId).IsRequired();

            builder.Property(u => u.UserId).IsRequired();
        }
    }
}