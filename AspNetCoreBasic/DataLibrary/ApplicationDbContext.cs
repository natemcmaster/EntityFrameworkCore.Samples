using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options) { }

        public virtual DbSet<Blog> Blogs { get; set; }
    }
}