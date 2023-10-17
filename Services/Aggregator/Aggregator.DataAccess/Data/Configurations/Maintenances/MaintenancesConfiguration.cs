using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations.Maintenances
{
    internal class MaintenancesConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);

            builder.Property(x => x.MaintenanceTypeId).IsRequired();

            builder.Property(x => x.ContributorId).IsRequired();

            builder.HasOne(c => c.MaintenanceType)
                .WithMany()
                .HasForeignKey(c => c.MaintenanceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
