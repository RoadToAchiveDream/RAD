using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.Domain.Entities;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.DataAccess.Contexts;
public class RadDbContext : DbContext
{
    public RadDbContext(DbContextOptions<RadDbContext> options) : base(options) { }

    #region DbSet Properties
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Goal> Goals { get; set; }
    #endregion
}
