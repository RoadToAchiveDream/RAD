using AutoMapper;
using RAD.DTOs.Tasks;
using RAD.Services.Configurations;
using RAD.Services.Services.Tasks;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Tasks;
using Task = RAD.Domain.Entities.Task;

namespace RAD.WebApi.ApiServices.Tasks;

public class TaskApiService(
    IMapper mapper,
    ITaskService taskService,
    TaskCreateModelValidator createModelValidator,
    TaskUpdateModelValidator updateModelValidator) : ITaskApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await taskService.DeleteAsync(id);
    }

    public async ValueTask<IEnumerable<TaskViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var tasks = await taskService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }

    public async ValueTask<TaskViewModel> GetAsync(long id)
    {
        var task = await taskService.GetByIdAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }

    public async ValueTask<TaskViewModel> PostAsync(TaskCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.CreateAsync(mapper.Map<Task>(model));
        return mapper.Map<TaskViewModel>(task);
    }

    public async ValueTask<TaskViewModel> PutAsync(long id, TaskUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.UpdateAsync(id, mapper.Map<Task>(model));
        return mapper.Map<TaskViewModel>(task);
    }
}
