using HardwareHero.Shared.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HardwareHero.Shared.Data.Configurations
{
    // TODO: ?
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(c => c.RegistrationDate).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(256);
        }
    }
}
