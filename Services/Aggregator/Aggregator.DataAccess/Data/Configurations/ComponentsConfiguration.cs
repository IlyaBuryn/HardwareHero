using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.DataAccess.Data.Configurations
{
    internal class ComponentsConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Description).HasMaxLength(1023);

            builder.HasData
            (
                new Component()
                {
                    Id = new Guid("0712d311-71e5-4c5b-8f80-1b1b08180851"),
                    Name = "Graphics Card #1",
                    Description = "Configuration items just for test",
                    ImageList = new List<string> { "nothing", "nothing", "nothing" },
                    SpecificationDictionary = new Dictionary<string, string> { { "ch1", "Great" }, { "ch2", "Bad" }, { "ch3", "Normal" } },
                    InitialPrice = 8.90m,
                },
                new Component()
                {
                    Id = new Guid("17bb6742-6611-4865-99f4-222610fb1b88"),
                    Name = "Processor #2",
                    Description = "Configuration items just for test",
                    ImageList = new List<string> { "nothing", "nothing", "nothing", "aaaand//nothing" },
                    SpecificationDictionary = new Dictionary<string, string> { { "ch1", "Wo" }, { "ch2", "Ah" }, { "ch3", "Oi" } },
                    InitialPrice = 30000m,
                }
            );
        }
    }
}
