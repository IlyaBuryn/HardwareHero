using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorExcellenceConfiguration
        : IEntityTypeConfiguration<ContributorExcellence>
    {
        public void Configure(EntityTypeBuilder<ContributorExcellence> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(128);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Logo).IsRequired().HasMaxLength(512);

            builder.HasData
            (
                new ContributorExcellence()
                {
                    Id = new Guid("3f46062f-56d8-4897-a37f-ff4e920b2d73"),
                    Name = "Test Name",
                    Logo = string.Empty,
                }
            );
        }
    }
}
