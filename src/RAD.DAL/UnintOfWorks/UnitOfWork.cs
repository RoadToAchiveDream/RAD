using RAD.DAL.Contexts;
using RAD.DAL.Repositories;
using RAD.Domain.Entities;
using Task = RAD.Domain.Entities.Task;

namespace RAD.DAL.UnintOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;

    public IRepository<User> Users { get; }
    public IRepository<Goal> Goals { get; }
    public IRepository<Habit> Habits { get; }
    public IRepository<Note> Notes { get; }
    public IRepository<NoteCategory> NoteCategories { get; }
    public IRepository<Task> Tasks { get; }
    public IRepository<TaskCategory> TaskCategories { get; }
    public IRepository<Event> Events { get; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;

        Users = new Repository<User>(context);
        Goals = new Repository<Goal>(context);
        Habits = new Repository<Habit>(context);
        Notes = new Repository<Note>(context);
        NoteCategories = new Repository<NoteCategory>(context);
        Tasks = new Repository<Task>(context);
        TaskCategories = new Repository<TaskCategory>(context);
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
