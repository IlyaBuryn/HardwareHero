using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations.Maintenances
{
    internal class MaintenanceLocalReviewsConfiguration : IEntityTypeConfiguration<MaintenanceLocalReview>
    {
        public void Configure(EntityTypeBuilder<MaintenanceLocalReview> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.MaintenanceId).IsRequired();

            builder.Property(x => x.Timestamp).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}
