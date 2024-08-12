namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentAttributesConfiguration : IEntityTypeConfiguration<ComponentAttributes>
    {
        public void Configure(EntityTypeBuilder<ComponentAttributes> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.ComponentId).IsRequired();

            builder.Property(c => c.AttributeName).IsRequired();

            builder.Property(c => c.AttributeValue).IsRequired();

            builder.HasOne(c => c.Component)
                .WithMany(c => c.ComponentAttributes)
                .HasForeignKey(c => c.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
