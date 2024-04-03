using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.Domain.Entities;

namespace RAD_BackEnd.DataAccess.Contexts;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Goal> Goals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

}
