namespace Mission08_Team0303.Data;

using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<ToDoTask> Tasks { get; set; }
}