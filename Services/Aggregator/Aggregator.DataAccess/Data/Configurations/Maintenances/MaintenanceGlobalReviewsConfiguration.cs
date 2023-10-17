using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations.Maintenances
{
    internal class MaintenanceGlobalReviewsConfiguration : IEntityTypeConfiguration<MaintenanceGlobalReview>
    {
        public void Configure(EntityTypeBuilder<MaintenanceGlobalReview> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.AuthorName).IsRequired();

            builder.Property(x => x.MaintenanceId).IsRequired();

            builder.Property(x => x.Timestamp).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}
