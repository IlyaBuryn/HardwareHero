using Identity.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Shared.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);

            builder.Property(u => u.FirstName).HasMaxLength(128);

            builder.Property(u => u.LastName).HasMaxLength(128);
        }
    }
}