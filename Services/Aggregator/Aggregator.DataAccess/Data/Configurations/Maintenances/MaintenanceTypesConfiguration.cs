using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations.Maintenances
{
    internal class MaintenanceTypesConfiguration : IEntityTypeConfiguration<MaintenanceType>
    {
        public void Configure(EntityTypeBuilder<MaintenanceType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        }
    }
}
