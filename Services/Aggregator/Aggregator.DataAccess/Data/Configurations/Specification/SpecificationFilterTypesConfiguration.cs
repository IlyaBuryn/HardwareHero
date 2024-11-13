using Aggregator.DataAccess.Models.Specifications;

namespace Aggregator.DataAccess.Data.Configurations.Specification
{
    internal class SpecificationFilterTypesConfiguration : IEntityTypeConfiguration<SpecificationFilterType>
    {
        public void Configure(EntityTypeBuilder<SpecificationFilterType> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
