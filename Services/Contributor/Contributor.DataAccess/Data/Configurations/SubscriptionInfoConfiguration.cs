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
                    PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                    ContributorId = new Guid("e61701f7-2c95-4986-bde9-0c18975e6cd6"),
                    RenewalDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                },
                new SubscriptionInfo()
                {
                    Id = new Guid("751d41e0-ebfa-435a-867c-8f00de10465f"),
                    PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                    ContributorId = new Guid("74bf3426-0a22-4c7a-ba2a-478a95363d46"),
                    RenewalDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                },
                new SubscriptionInfo()
                {
                    Id = new Guid("9fb5a64e-8211-4a6a-835d-ece23056ff31"),
                    PlanId = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                    ContributorId = new Guid("9c3436f0-0d5e-4295-9da8-9d61674bc14b"),
                    RenewalDate = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddDays(30),
                }
            );
        }
    }
}
