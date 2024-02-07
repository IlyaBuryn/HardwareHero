namespace Contributor.DataAccess.Data.Configurations
{
    internal class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Price).HasColumnType("decimal(18,4)").IsRequired();

            builder.Property(c => c.PriorityLevel).IsRequired();

            builder.Property(c => c.DaysCount).IsRequired().HasDefaultValue(ValidationValues.SubscriptionDefaultDays);
        }
    }
}
