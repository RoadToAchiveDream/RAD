﻿using Microsoft.EntityFrameworkCore;
using RAD.Domain.Entities;
using Task = RAD.Domain.Entities.Task;

namespace RAD.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region DbSet Properties
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<NoteCategory> NoteCategories { get; set; }
    public DbSet<Goal> Goals { get; set; }

    public DbSet<Cashbook> Cashbooks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    #endregion
}
