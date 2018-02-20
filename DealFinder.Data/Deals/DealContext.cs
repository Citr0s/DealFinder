using Microsoft.EntityFrameworkCore;

namespace DealFinder.Data.Deals
{
    public class DealContext : DbContext
    {
        public DealContext(DbContextOptions<DealContext> options) : base(options) { }

        public DbSet<DealRecord> Deals { get; set; }
    }
}
