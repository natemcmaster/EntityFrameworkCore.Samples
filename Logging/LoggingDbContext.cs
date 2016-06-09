using Microsoft.EntityFrameworkCore;

namespace LoggingSample
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Ship> Ships { get; set; }
    }
}