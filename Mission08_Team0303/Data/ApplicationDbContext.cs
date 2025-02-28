using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Models;

namespace Mission08_Team0303.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Home" },
                new Category { Id = 2, Name = "School" },
                new Category { Id = 3, Name = "Work" },
                new Category { Id = 4, Name = "Church" }
            );
        }
    }
}