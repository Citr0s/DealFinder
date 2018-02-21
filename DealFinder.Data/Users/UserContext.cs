using DealFinder.Data.Users.Repository;
using Microsoft.EntityFrameworkCore;

namespace DealFinder.Data.Users
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<UserRecord> Users { get; set; }
    }
}