using Aggregator.DataAccess.Models.Components;

namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentAttributeConfiguration : IEntityTypeConfiguration<ComponentAttribute>
    {
        public void Configure(EntityTypeBuilder<ComponentAttribute> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.ComponentId).IsRequired();

            builder.Property(c => c.SpecificationAttributeId).IsRequired();

            builder.HasOne(c => c.Component)
                .WithMany(c => c.ComponentAttributes)
                .HasForeignKey(c => c.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
