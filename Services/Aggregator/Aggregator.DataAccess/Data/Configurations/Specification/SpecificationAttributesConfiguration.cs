using Aggregator.DataAccess.Models.Specifications;

namespace Aggregator.DataAccess.Data.Configurations.Specification
{
    internal class SpecificationAttributesConfiguration : IEntityTypeConfiguration<SpecificationAttribute>
    {
        public void Configure(EntityTypeBuilder<SpecificationAttribute> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.ComponentTypeId).IsRequired();

            builder.Property(c => c.SpecificationCategoryId).IsRequired();

            builder.Property(c => c.Key).IsRequired();
        }
    }
}
