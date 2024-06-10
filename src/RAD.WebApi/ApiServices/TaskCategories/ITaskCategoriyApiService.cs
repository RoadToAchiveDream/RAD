using RAD.Services.Configurations;
using RAD.WebApi.DTOs.TaskCategories;

namespace RAD.WebApi.ApiServices.TaskCategories;

public interface ITaskCategoryApiService
{
    public ValueTask<TaskCategoryViewModel> PostAsync(TaskCategoryCreateModel model);
    public ValueTask<TaskCategoryViewModel> PutAsync(long id, TaskCategoryUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<TaskCategoryViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<TaskCategoryViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<TaskCategoryViewModel> AddTaskToCategoryAsync(long categoryId, long taskId);
    public ValueTask<TaskCategoryViewModel> RemoveTaskFromCategoryAsync(long categoryId, long taskId);
    public ValueTask<TaskCategoryViewModel> GetCategoryByNameAsync(string name);
}
