using RAD_BackEnd.DataAccess.Contexts;
using RAD_BackEnd.DataAccess.Repositories;
using RAD_BackEnd.Domain.Entities;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.DataAccess.UnintOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly RadDbContext context;

    public IRepository<User> Users { get; }
    public IRepository<Goal> Goals { get; }
    public IRepository<Habit> Habits { get; }
    public IRepository<Note> Notes { get; }
    public IRepository<Task> Tasks { get; }
    public IRepository<Event> Events { get; }

    public UnitOfWork(RadDbContext context)
    {
        this.context = context;

        Users = new Repository<User>(context);
        Goals = new Repository<Goal>(context);
        Habits = new Repository<Habit>(context);
        Notes = new Repository<Note>(context);
        Tasks = new Repository<Task>(context);
        Events = new Repository<Event>(context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
