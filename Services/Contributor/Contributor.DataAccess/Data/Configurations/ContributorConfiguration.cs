using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorConfiguration : IEntityTypeConfiguration<ContributorModel>
    {
        public void Configure(EntityTypeBuilder<ContributorModel> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.UserId).IsRequired();
            builder.HasIndex(c => c.UserId).IsUnique();

            builder.Property(c => c.Region).IsRequired().HasMaxLength(128);

            builder.HasData
            (
                new ContributorModel()
                {
                    Id = new Guid("ef12555d-c912-402d-a045-148091680d9a"),
                    UserId = new Guid("8fe35832-874a-447b-a076-6e030b87d7eb"),
                    Region = "Poland",
                    SubscriptionInfoId = new Guid("cf7a198c-c551-456f-a519-e8679f3d0662"),
                    ContributorExcellenceId = new Guid("63427830-5841-4378-9c7f-2381c177a3f7"),
                }
            );
        }
    }
}
