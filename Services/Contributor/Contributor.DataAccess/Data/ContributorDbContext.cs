using Microsoft.EntityFrameworkCore;
using HardwareHero.Services.Shared.Models.Contributor;
using Contributor.DataAccess.Data.Configurations;

namespace Contributor.DataAccess.Data
{
    public class ContributorDbContext : DbContext
    {
        public ContributorDbContext(DbContextOptions<ContributorDbContext> options) 
            : base(options) { }

        public DbSet<ContributorModel> Contributors { get; set; }
        public DbSet<ContributorExcellence> ContributorExcellences { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SubscriptionInfo> SubscriptionInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new ChatRoomConfiguration());
            modelBuilder.ApplyConfiguration(new ContributorConfiguration());
            modelBuilder.ApplyConfiguration(new ContributorExcellenceConfiguration());
            modelBuilder.ApplyConfiguration(new ReferencesConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionInfoConfiguration());
        }
    }
}
