using Microsoft.EntityFrameworkCore;

namespace SatbayevUsers_WebAPI.Data
{
    public class SatbayevUsersDbContext(DbContextOptions<SatbayevUsersDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
