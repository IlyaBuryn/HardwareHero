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

            builder.Property(c => c.PlanId).IsRequired()
                .HasDefaultValue(new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"));

            builder.Property(c => c.ContributorId).IsRequired();

            builder.Property(c => c.RenewalDate).IsRequired().HasDefaultValue(DateTime.Now);

            builder.Property(c => c.ExpiryDate).IsRequired().HasDefaultValue(DateTime.Now.AddDays(30));


            builder.HasData
            (
                new SubscriptionInfo()
                {
                    Id = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                    PlanId = new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"),
                    ContributorId = new Guid("ef12555d-c912-402d-a045-148091680d9a"),
                    RenewalDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                }
            );
        }
    }
}
