namespace Aggregator.DataAccess.Data.Configurations.Components
{
    internal class ComponentLocalReviewsConfiguration : IEntityTypeConfiguration<ComponentLocalReview>
    {
        public void Configure(EntityTypeBuilder<ComponentLocalReview> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.UserId).IsRequired();

            builder.Property(c => c.ComponentId).IsRequired();

            builder.Property(c => c.Timestamp).IsRequired().HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Rating).HasMaxLength(5);
        }
    }
}
