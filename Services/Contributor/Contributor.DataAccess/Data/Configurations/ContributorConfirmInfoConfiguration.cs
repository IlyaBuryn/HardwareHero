namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorConfirmInfoConfiguration : IEntityTypeConfiguration<ContributorConfirmInfo>
    {
        public void Configure(EntityTypeBuilder<ContributorConfirmInfo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.IsConfirmed).HasDefaultValue(false);

            builder.Property(c => c.TimeStamp).HasDefaultValue(DateTime.Now);
        }
    }
}
