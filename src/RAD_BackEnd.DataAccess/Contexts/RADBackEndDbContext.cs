using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.Domain.Entities;

namespace RAD_BackEnd.DataAccess.Contexts;
public class RADBackEndDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<PlansTasks> PlansTasks { get; set; }
    public DbSet<Plans> Plans { get; set; }
    public DbSet<Notes> Notes { get; set; }
    public DbSet<DreamPlans> DreamPlans { get; set; }
    public DbSet<Dream> Dreams { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Fluent API
    }

}
