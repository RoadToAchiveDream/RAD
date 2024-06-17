using RAD.Services.Configurations;
using RAD.WebApi.DTOs.Tasks;

namespace RAD.WebApi.ApiServices.Tasks;

public interface ITaskApiService
{
    public ValueTask<TaskViewModel> PostAsync(TaskCreateModel model);
    public ValueTask<TaskViewModel> PutAsync(long id, TaskUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<TaskViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<TaskViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<TaskViewModel> SetTaskDueDateAsync(long id, SetTaskDueDateModel model);
    public ValueTask<TaskViewModel> SetTaskReminderAsync(long id, SetTaskReminderModel model);
    public ValueTask<TaskViewModel> SetTaskPriorityAsync(long id, SetTaskPriorityModel model);
    public ValueTask<TaskViewModel> SetTaskReccuringAsync(long id, SetTaskReccuringModel model);
    public ValueTask<TaskViewModel> SetTaskStatusAsync(long id, SetTaskStatusModel model);

    public ValueTask<TaskViewModel> UnsetTaskDueDateAsync(long id);
    public ValueTask<TaskViewModel> UnsetTaskReminderAsync(long id);
    public ValueTask<TaskViewModel> UnsetTaskStatusAsync(long id);
    public ValueTask<TaskViewModel> UnsetTaskPriorityAsync(long id);
    public ValueTask<TaskViewModel> UnsetTaskReccuringAsync(long id);

    public ValueTask<IEnumerable<TaskViewModel>> GetTasksByDueDateAsync(PaginationParams @params, Filter filter, DateTime dueDate);
    public ValueTask<IEnumerable<TaskViewModel>> GetTasksByReminderAsync(PaginationParams @params, Filter filter, DateTime reminder);
    public ValueTask<IEnumerable<TaskViewModel>> GetTasksByStatusAsync(PaginationParams @params, Filter filter, string status);
    public ValueTask<IEnumerable<TaskViewModel>> GetTasksByPriorityAsync(PaginationParams @params, Filter filter, string priority);
    public ValueTask<IEnumerable<TaskViewModel>> GetTasksByReccuringAsync(PaginationParams @params, Filter filter, string reccuring);
}
