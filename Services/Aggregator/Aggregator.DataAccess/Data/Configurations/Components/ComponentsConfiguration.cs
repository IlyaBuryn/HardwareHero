namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentsConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Description).HasMaxLength(1024);

            builder.Property(c => c.ComponentTypeId).IsRequired();

            builder.HasMany(c => c.ComponentImages)
                .WithOne(c => c.Component)
                .HasForeignKey(k => k.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ComponentAttributes)
                .WithOne(c => c.Component)
                .HasForeignKey(k => k.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ComponentType)
                .WithMany()
                .HasForeignKey(c => c.ComponentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
