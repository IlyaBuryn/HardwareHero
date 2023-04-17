using Microsoft.EntityFrameworkCore;
using HardwareHero.Services.Shared.Models.Contributor;
using Contributor.DataAccess.Data.Configurations;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
