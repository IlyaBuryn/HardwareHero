using Aggregator.DataAccess.Models.Components;

namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentImageConfiguration : IEntityTypeConfiguration<ComponentImage>
    {
        public void Configure(EntityTypeBuilder<ComponentImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            //builder.Property(x => x.ComponentId).IsRequired();
        }
    }
}
