namespace Contributor.DataAccess.Data.Configurations
{
    internal class SubscriptionPlanInfoConfiguration : IEntityTypeConfiguration<SubscriptionPlanInfo>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlanInfo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.PlanId).IsRequired();

            builder.Property(c => c.RenewalDate).IsRequired().HasDefaultValue(DateTime.Now);

            builder.Property(c => c.ExpiryDate).IsRequired()
                .HasDefaultValue(DateTime.Now.AddDays(ValidationValues.SubscriptionDefaultDays));

            builder.HasOne(spi => spi.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(spi => spi.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
