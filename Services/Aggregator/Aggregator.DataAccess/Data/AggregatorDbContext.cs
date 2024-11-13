using Aggregator.DataAccess.Models.Components;
using Aggregator.DataAccess.Models.Specifications;
using System.Reflection;

namespace Aggregator.DataAccess.Data
{
    internal class AggregatorDbContext : DbContext
    {
        public AggregatorDbContext(DbContextOptions<AggregatorDbContext> options) 
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<SpecificationCategory> SpecificationCategories { get; set; }
        public DbSet<SpecificationFilterType> SpecificationFilterTypes { get; set; }

        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ComponentMetric> ComponentMetrics { get; set; }

        public DbSet<SpecificationAttribute> SpecificationAttributes { get; set; }

        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentAttribute> ComponentAttributes { get; set; }
        public DbSet<ComponentImage> ComponentImages { get; set; }
        public DbSet<ComponentGlobalReview> ComponentGlobalReviews { get; set; }
        public DbSet<ComponentLocalReview> ComponentLocalReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
