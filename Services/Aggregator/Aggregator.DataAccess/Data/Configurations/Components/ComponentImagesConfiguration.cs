namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentImagesConfiguration : IEntityTypeConfiguration<ComponentImages>
    {
        public void Configure(EntityTypeBuilder<ComponentImages> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ComponentId).IsRequired();

            builder.Property(x => x.Image).IsRequired();

            builder.HasOne(c => c.Component)
                .WithMany(c => c.ComponentImages)
                .HasForeignKey(c => c.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
