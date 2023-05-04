using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
