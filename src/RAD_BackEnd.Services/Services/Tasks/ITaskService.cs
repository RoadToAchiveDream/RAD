using RAD_BackEnd.Services.Configurations;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.Services.Services.Tasks;

public interface ITaskService
{
    ValueTask<Task> CreateAsync(Task task);
    ValueTask<Task> UpdateAsync(long id, Task task);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Task> GetByIdAsync(long id);
    ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
