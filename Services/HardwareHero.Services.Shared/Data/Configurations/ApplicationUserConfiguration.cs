using HardwareHero.Services.Shared.Models.UserManagementService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HardwareHero.Services.Shared.Data.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(c => c.RegistrationDate).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(256);
        }
    }
}
