using System.Reflection;

namespace Contributor.DataAccess.Data
{
    public class ContributorDbContext : DbContext
    {
        public ContributorDbContext(DbContextOptions<ContributorDbContext> options) 
            : base(options) { }

        public DbSet<ContributorModel> Contributors { get; set; }
        public DbSet<ContributorExcellence> ContributorExcellences { get; set; }
        public DbSet<ContributorConfirmInfo> ContributorConfirmInfos { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SubscriptionPlanInfo> SubscriptionPlanInfos { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
