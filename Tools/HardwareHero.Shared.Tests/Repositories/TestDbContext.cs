using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Tests.Repositories
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<RelatedTestEntity> RelatedTestEntities { get; set; }
        public DbSet<DeepRelatedTestEntity> DeepRelatedTestEntities { get; set; }
        public DbSet<AnotherTestEntity> AnotherTestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestEntity>().ToTable("TestEntities");
            modelBuilder.Entity<RelatedTestEntity>().ToTable("RelatedTestEntities");
        }

        internal static TestDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new TestDbContext(options);
        }
    }
}
