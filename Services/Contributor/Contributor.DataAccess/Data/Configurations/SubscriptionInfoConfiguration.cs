using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class SubscriptionInfoConfiguration : IEntityTypeConfiguration<SubscriptionInfo>
    {
        public void Configure(EntityTypeBuilder<SubscriptionInfo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.PlanId).IsRequired();

            builder.HasData
            (
                new SubscriptionInfo()
                {
                    Id = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                    PlanId = new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"),
                    RenewalDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(30),
                }
            );
        }
    }
}
