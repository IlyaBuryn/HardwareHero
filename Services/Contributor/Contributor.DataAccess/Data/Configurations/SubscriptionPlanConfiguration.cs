using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Price).IsRequired();

            builder.Property(c => c.PriorityLevel).IsRequired();

            builder.Property(c => c.DaysCount).IsRequired().HasDefaultValue(30);

            builder.HasData
            (
                new SubscriptionPlan()
                {
                    Id = new Guid("ca7f44ac-ec3c-4caa-9ee7-dc1c6550a681"),
                    Price = 0m,
                    PriorityLevel = 0,
                    DaysCount = 30
                },
                new SubscriptionPlan()
                {
                    Id = new Guid("d4c656b7-5a3d-4318-bbf9-37438750e542"),
                    Price = 100.0m,
                    PriorityLevel = 1,
                    DaysCount = 30
                },
                new SubscriptionPlan()
                {
                    Id = new Guid("39bc7661-8341-40e6-9065-a77d31926484"),
                    Price = 500.0m,
                    PriorityLevel = 2,
                    DaysCount = 30
                }
            );
        }
    }
}
