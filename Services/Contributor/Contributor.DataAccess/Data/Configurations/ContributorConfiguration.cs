namespace Contributor.DataAccess.Data.Configurations
{
    internal class ContributorConfiguration : IEntityTypeConfiguration<ContributorModel>
    {
        public void Configure(EntityTypeBuilder<ContributorModel> builder)
        {
            builder.ToTable("Contributors");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.UserId).IsRequired();
            builder.HasIndex(c => c.UserId).IsUnique();

            builder.Property(c => c.ContributorExcellenceId).IsRequired();
            builder.HasIndex(c => c.ContributorExcellenceId).IsUnique();
            builder.HasIndex(c => c.ContributorExcellenceId).HasFilter("[ContributorExcellenceId] IS NOT NULL");

            builder.HasIndex(c => c.ContributorConfirmInfoId).IsUnique();
            builder.HasIndex(c => c.ContributorConfirmInfoId).HasFilter("[ContributorConfirmInfoId] IS NOT NULL");

            builder.HasIndex(c => c.SubscriptionPlanInfoId).IsUnique();
            builder.HasIndex(c => c.SubscriptionPlanInfoId).HasFilter("[SubscriptionPlanInfoId] IS NOT NULL");

            builder.HasOne(cm => cm.SubscriptionPlanInfo)
                .WithMany()
                .HasForeignKey(cm => cm.SubscriptionPlanInfoId)
                .OnDelete(DeleteBehavior.SetNull);

            // TODO: ContributorConfirmInfo cascade and ContributorExcellence cascade
        }
    }
}
