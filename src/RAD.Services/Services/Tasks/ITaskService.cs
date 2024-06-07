using RAD.Services.Configurations;
using Task = RAD.Domain.Entities.Task;

namespace RAD.Services.Services.Tasks;

public interface ITaskService
{
    ValueTask<Task> CreateAsync(Task task);
    ValueTask<Task> UpdateAsync(long id, Task task);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Task> GetByIdAsync(long id);
    ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    ValueTask<Task> SetDueDate(long id, DateTime dueDate);
    ValueTask<Task> SetReminder(long id, DateTime reminder);
    ValueTask<Task> SetStatus(long id, string status);
    ValueTask<Task> SetPriority(long id, string priority);
    ValueTask<Task> SetReccuring(long id, string reccuring);

    ValueTask<Task> UnsetDueDate(long id);
    ValueTask<Task> UnsetReminder(long id);
    ValueTask<Task> UnsetStatus(long id);
    ValueTask<Task> UnsetPriority(long id);
    ValueTask<Task> UnsetReccuring(long id);
}
