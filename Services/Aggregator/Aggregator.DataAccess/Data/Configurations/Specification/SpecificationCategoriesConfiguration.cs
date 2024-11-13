using Aggregator.DataAccess.Models.Specifications;

namespace Aggregator.DataAccess.Data.Configurations.Specification
{
    internal class SpecificationCategoriesConfiguration : IEntityTypeConfiguration<SpecificationCategory>
    {
        public void Configure(EntityTypeBuilder<SpecificationCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Priority).HasDefaultValue(1);
        }
    }
}
