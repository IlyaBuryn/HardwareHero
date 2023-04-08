using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations
{
    public class ComponentReviewsConfiguration : IEntityTypeConfiguration<ComponentReview>
    {
        public void Configure(EntityTypeBuilder<ComponentReview> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).HasMaxLength(128);

            builder.Property(c => c.ContributorName).HasMaxLength(128);

            builder.HasData
            (
                new ComponentReview()
                {
                    Id = new Guid("665cba20-d56b-47ba-be57-4672014a91f6"),
                    Name = "Bob",
                    ComponentId = new Guid("0712d311-71e5-4c5b-8f80-1b1b08180851"),
                    Date = DateTime.Now,
                    Recommended = true,
                    Text = "Very good, very nice!",
                    ContributorName = "Amazon",
                    ContributorLogo = null
                }
            ); ;
        }
    }
}
