using HardwareHero.Services.Shared.Models.Aggregator;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aggregator.DataAccess.Data
{
    internal class AggregatorDbContext : DbContext
    {
        public AggregatorDbContext(DbContextOptions<AggregatorDbContext> options) 
            : base(options)
        { }

        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentAttributes> ComponentAttributes { get; set; }
        public DbSet<ComponentImages> ComponentImages { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<ComponentGlobalReview> ComponentGlobalReviews { get; set; }
        public DbSet<ComponentLocalReview> ComponentLocalReviews { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
        public DbSet<MaintenanceGlobalReview> MaintenanceGlobalReviews { get; set; }
        public DbSet<MaintenanceLocalReview> MaintenanceLocalReviews { get; set; }

        public DbSet<ComponentViews> ComponentViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
