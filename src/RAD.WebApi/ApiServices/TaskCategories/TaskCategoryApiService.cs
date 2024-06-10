using AutoMapper;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Services.TaskCategories;
using RAD.WebApi.DTOs.TaskCategories;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.TaskCategories;

namespace RAD.WebApi.ApiServices.TaskCategories;

public class TaskCategoryApiService(
    IMapper mapper,
    ITaskCategoryService taskCategoryService,
    TaskCategoryCreateModelValidator createModelValidator,
    TaskCategoryUpdateModelValidator updateModelValidator) : ITaskCategoryApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await taskCategoryService.DeleteAsync(id);
    }

    public async ValueTask<TaskCategoryViewModel> GetAsync(long id)
    {
        var taskCategory = await taskCategoryService.GetByIdAsync(id);
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }

    public async ValueTask<IEnumerable<TaskCategoryViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var taskCategories = await taskCategoryService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<TaskCategoryViewModel>>(taskCategories);
    }

    public async ValueTask<TaskCategoryViewModel> PostAsync(TaskCategoryCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var taskCategory = await taskCategoryService.CreateAsync(mapper.Map<TaskCategory>(model));
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }

    public async ValueTask<TaskCategoryViewModel> PutAsync(long id, TaskCategoryUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var taskCategory = await taskCategoryService.UpdateAsync(id, mapper.Map<TaskCategory>(model));
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }

    public async ValueTask<TaskCategoryViewModel> AddTaskToCategoryAsync(long categoryId, long taskId)
    {
        var taskCategory = await taskCategoryService.AddTaskToCategoryAsync(categoryId, taskId);
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }

    public async ValueTask<TaskCategoryViewModel> RemoveTaskFromCategoryAsync(long categoryId, long taskId)
    {
        var taskCategory = await taskCategoryService.RemoveTaskFromCategoryAsync(categoryId, taskId);
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }

    public async ValueTask<TaskCategoryViewModel> GetCategoryByNameAsync(string name)
    {
        var taskCategory = await taskCategoryService.GetCategoryByNameAsync(name);
        return mapper.Map<TaskCategoryViewModel>(taskCategory);
    }
}
