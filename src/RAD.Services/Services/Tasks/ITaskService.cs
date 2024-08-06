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

    public ValueTask<Task> SetDueDateAsync(long id, DateTime dueDate);
    public ValueTask<Task> SetReminderAsync(long id, DateTime reminder);
    public ValueTask<Task> SetStatusAsync(long id, string status);
    public ValueTask<Task> SetPriorityAsync(long id, string priority);
    public ValueTask<Task> SetReccuringAsync(long id, string reccuring);

    public ValueTask<Task> UnsetDueDateAsync(long id);
    public ValueTask<Task> UnsetReminderAsync(long id);
    public ValueTask<Task> UnsetStatusAsync(long id);
    public ValueTask<Task> UnsetReccuringAsync(long id);
    public ValueTask<Task> UnsetReccuring(long id);

    public ValueTask<Task> SetIsCompletedAsync(long id);
    public ValueTask<Task> UnsetIsCompletedAsync(long id);

    public ValueTask<Task> SetCategoryIdAsync(long taskId, long categoryId);
    public ValueTask<Task> UnsetCategoryIdAsync(long taskId);

    public ValueTask<IEnumerable<Task>> GetAllCompletedAsync(PaginationParams @params, Filter filter);
    public ValueTask<IEnumerable<Task>> GetAllNotCompletedAsync(PaginationParams @params, Filter filter);

    public ValueTask<IEnumerable<Task>> GetByDueDateAsync(PaginationParams @params, Filter filter, DateTime dueDate);
    public ValueTask<IEnumerable<Task>> GetByReminderAsync(PaginationParams @params, Filter filter, DateTime reminder);
    public ValueTask<IEnumerable<Task>> GetByStatusAsync(PaginationParams @params, Filter filter, string status);
    public ValueTask<IEnumerable<Task>> GetByPriorityAsync(PaginationParams @params, Filter filter, string priority);
    public ValueTask<IEnumerable<Task>> GetByReccuringAsync(PaginationParams @params, Filter filter, string reccuring);
}
