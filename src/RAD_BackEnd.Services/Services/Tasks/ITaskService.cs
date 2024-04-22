using RAD_BackEnd.DTOs.Tasks;

namespace RAD_BackEnd.Services.Services.Tasks;

public interface ITaskService
{
    ValueTask<TaskViewModel> CreateAsync(TaskCreateModel task);
    ValueTask<TaskViewModel> UpdateAsync(long id, TaskUpdateModel task);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TaskViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<TaskViewModel>> GetAllAsync();
}
