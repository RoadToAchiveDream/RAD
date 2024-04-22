using RAD_BackEnd.DataAccess.Repositories;
using RAD_BackEnd.Domain.Entities;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.DataAccess.UnintOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Goal> Goals { get; }
    IRepository<Habit> Habits { get; }
    IRepository<Note> Notes { get; }
    IRepository<Task> Tasks { get; }
    IRepository<Event> Events { get; }
}
