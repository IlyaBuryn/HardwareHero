namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentTypesConfiguration : IEntityTypeConfiguration<ComponentType>
    {
        public void Configure(EntityTypeBuilder<ComponentType> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(32);

            builder.Property(c => c.FullName).HasMaxLength(128);

            builder.Property(c => c.Description).HasMaxLength(1024);
        }
    }
}
