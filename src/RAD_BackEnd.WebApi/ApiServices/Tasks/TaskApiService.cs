using AutoMapper;
using RAD_BackEnd.DTOs.Tasks;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Services.Tasks;
using RAD_BackEnd.WebApi.Extensions;
using RAD_BackEnd.WebApi.Validators.Tasks;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.WebApi.ApiServices.Tasks;

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
