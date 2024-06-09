using RAD.DAL.Repositories;
using RAD.Domain.Entities;
using Task = RAD.Domain.Entities.Task;

namespace RAD.DAL.UnintOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Goal> Goals { get; }
    IRepository<Habit> Habits { get; }
    IRepository<Note> Notes { get; }
    IRepository<NoteCategory> NoteCategories { get; }
    IRepository<Task> Tasks { get; }
    IRepository<TaskCategory> TaskCategories { get; }
    IRepository<Event> Events { get; }

    ValueTask<bool> SaveAsync();
}
