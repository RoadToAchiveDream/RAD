using RAD.Services.Configurations;
using Task = RAD.Domain.Entities.Task;

namespace RAD.Services.Services.Tasks;

public interface ITaskService
{
    public ValueTask<Task> CreateAsync(Task task);
    public ValueTask<Task> UpdateAsync(long id, Task task);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Task> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<Task> SetDueDate(long id, DateTime dueDate);
    public ValueTask<Task> SetReminder(long id, DateTime reminder);
    public ValueTask<Task> SetStatus(long id, string status);
    public ValueTask<Task> SetPriority(long id, string priority);
    public ValueTask<Task> SetReccuring(long id, string reccuring);


    public ValueTask<Task> UnsetDueDate(long id);
    public ValueTask<Task> UnsetReminder(long id);
    public ValueTask<Task> UnsetStatus(long id);
    public ValueTask<Task> UnsetPriority(long id);
    public ValueTask<Task> UnsetReccuring(long id);

    public ValueTask<IEnumerable<Task>> GetByDueDate(PaginationParams @params, Filter filter, DateTime dueDate);
    public ValueTask<IEnumerable<Task>> GetByReminder(PaginationParams @params, Filter filter, DateTime reminder);
    public ValueTask<IEnumerable<Task>> GetByStatus(PaginationParams @params, Filter filter, string status);
    public ValueTask<IEnumerable<Task>> GetByPriority(PaginationParams @params, Filter filter, string priority);
    public ValueTask<IEnumerable<Task>> GetByReccuring(PaginationParams @params, Filter filter, string reccuring);
}
