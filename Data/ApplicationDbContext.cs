using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
