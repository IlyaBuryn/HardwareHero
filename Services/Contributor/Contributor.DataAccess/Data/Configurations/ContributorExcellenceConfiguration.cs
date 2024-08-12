namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorExcellenceConfiguration
        : IEntityTypeConfiguration<ContributorExcellence>
    {
        public void Configure(EntityTypeBuilder<ContributorExcellence> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(ValidationValues.NameMaxLength);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Logo).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(ValidationValues.ContributorDescriptionMaxLength);

            builder.Property(c => c.Phone).IsRequired().HasMaxLength(ValidationValues.PhoneMaxLength);

            builder.Property(c => c.RegionId).IsRequired();
        }
    }
}
