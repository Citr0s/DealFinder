using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Tags.Repository;
using DealFinder.Data.Users.Repository;
using DealFinder.Data.Votes.Repository;
using Microsoft.EntityFrameworkCore;

namespace DealFinder.Data
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "server=localhost;database=deal_finder;username=postgres;password=password";
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DealTagRecord>()
                .HasKey(t => new { t.DealIdentifier, t.TagIdentifier });

            modelBuilder.Entity<TagRecord>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }

        public DbSet<DealRecord> Deals { get; set; }
        public DbSet<UserRecord> Users { get; set; }
        public DbSet<VoteRecord> Votes { get; set; }
        public DbSet<TagRecord> Tags { get; set; }
        public DbSet<DealTagRecord> DealTags { get; set; }
    }
}
