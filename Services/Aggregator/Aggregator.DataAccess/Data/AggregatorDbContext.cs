using Aggregator.DataAccess.Data.Configurations;
using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Data
{
    internal class AggregatorDbContext : DbContext
    {
        public AggregatorDbContext(DbContextOptions<AggregatorDbContext> options) : base(options)
        { }

        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentReview> ComponentReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ComponentsConfiguration());
            modelBuilder.ApplyConfiguration(new ComponentReviewsConfiguration());
        }
    }
}
