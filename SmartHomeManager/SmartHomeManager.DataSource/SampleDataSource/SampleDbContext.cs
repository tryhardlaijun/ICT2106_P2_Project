using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.SampleDomain.Entities;

namespace SmartHomeManager.DataSource.SampleDataSource
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Sample> Samples { get; set; }

        public SampleDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sample>().ToTable("Sample");
            modelBuilder.Ignore<Sample>();
        }

    }
}
