using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentGlobalReviewsConfiguration : IEntityTypeConfiguration<ComponentGlobalReview>
    {
        public void Configure(EntityTypeBuilder<ComponentGlobalReview> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.AuthorName).IsRequired();

            builder.Property(c => c.ComponentId).IsRequired();

            builder.Property(c => c.Timestamp).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}
