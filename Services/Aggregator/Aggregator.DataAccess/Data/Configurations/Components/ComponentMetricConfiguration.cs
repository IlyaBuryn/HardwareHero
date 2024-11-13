using Aggregator.DataAccess.Models.Components;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentMetricConfiguration : IEntityTypeConfiguration<ComponentMetric>
    {
        public void Configure(EntityTypeBuilder<ComponentMetric> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ViewsCount).HasDefaultValue(0);

            builder.Property(x => x.ReviewCount).HasDefaultValue(0);

            builder.Property(x => x.Rating).HasDefaultValue(0);

            builder.Property(x => x.MinPrice).HasDefaultValue(0);

            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
