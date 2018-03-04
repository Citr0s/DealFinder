using DealFinder.Data.Deals.Repository;
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

        public DbSet<DealRecord> Deals { get; set; }
        public DbSet<UserRecord> Users { get; set; }
        public DbSet<VoteRecord> Votes { get; set; }
    }
}
