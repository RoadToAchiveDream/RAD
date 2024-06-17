using AutoMapper;
using RAD.Services.Configurations;
using RAD.Services.Services.Tasks;
using RAD.WebApi.DTOs.Tasks;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Tasks;
using Task = RAD.Domain.Entities.Task;

namespace RAD.WebApi.ApiServices.Tasks;

public class TaskApiService(
    IMapper mapper,
    ITaskService taskService,
    TaskCreateModelValidator createModelValidator,
    TaskUpdateModelValidator updateModelValidator,
    SetTaskDueDateModelValidator setTaskDueDateModelValidator,
    SetTaskStatusModelValidator setTaskStatusModelValidator,
    SetTaskReccuringModelValidator setTaskReccuringModelValidator,
    SetTaskPriorityModelValidator setTaskPriorityModelValidator,
    SetTaskReminderModelValidator setTaskReminderModelValidator) : ITaskApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await taskService.DeleteAsync(id);
    }
    public async ValueTask<IEnumerable<TaskViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
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


    public async ValueTask<TaskViewModel> SetTaskDueDateAsync(long id, SetTaskDueDateModel model)
    {
        await setTaskDueDateModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.SetDueDateAsync(id, model.DueDate);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> SetTaskReminderAsync(long id, SetTaskReminderModel model)
    {
        await setTaskReminderModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.SetReminderAsync(id, model.Reminder);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> SetTaskPriorityAsync(long id, SetTaskPriorityModel model)
    {
        await setTaskPriorityModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.SetPriorityAsync(id, model.Priority);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> SetTaskReccuringAsync(long id, SetTaskReccuringModel model)
    {
        await setTaskReccuringModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.SetReccuringAsync(id, model.Reccuring);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> SetTaskStatusAsync(long id, SetTaskStatusModel model)
    {
        await setTaskStatusModelValidator.EnsureValidatedAsync(model);
        var task = await taskService.SetStatusAsync(id, model.Status);
        return mapper.Map<TaskViewModel>(task);
    }

    public async ValueTask<TaskViewModel> UnsetTaskDueDateAsync(long id)
    {
        var task = await taskService.UnsetDueDateAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> UnsetTaskReminderAsync(long id)
    {
        var task = await taskService.UnsetReminderAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> UnsetTaskStatusAsync(long id)
    {
        var task = await taskService.UnsetStatusAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> UnsetTaskPriorityAsync(long id)
    {
        var task = await taskService.UnsetReccuringAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }
    public async ValueTask<TaskViewModel> UnsetTaskReccuringAsync(long id)
    {
        var task = await taskService.UnsetReccuringAsync(id);
        return mapper.Map<TaskViewModel>(task);
    }

    public async ValueTask<IEnumerable<TaskViewModel>> GetTasksByDueDateAsync(PaginationParams @params, Filter filter, DateTime dueDate)
    {
        var tasks = await taskService.GetByDueDateAsync(@params, filter, dueDate);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }
    public async ValueTask<IEnumerable<TaskViewModel>> GetTasksByReminderAsync(PaginationParams @params, Filter filter, DateTime reminder)
    {
        var tasks = await taskService.GetByReminderAsync(@params, filter, reminder);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }
    public async ValueTask<IEnumerable<TaskViewModel>> GetTasksByStatusAsync(PaginationParams @params, Filter filter, string status)
    {
        var tasks = await taskService.GetByStatusAsync(@params, filter, status);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }
    public async ValueTask<IEnumerable<TaskViewModel>> GetTasksByPriorityAsync(PaginationParams @params, Filter filter, string priority)
    {
        var tasks = await taskService.GetByPriorityAsync(@params, filter, priority);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }
    public async ValueTask<IEnumerable<TaskViewModel>> GetTasksByReccuringAsync(PaginationParams @params, Filter filter, string reccuring)
    {
        var tasks = await taskService.GetByReccuringAsync(@params, filter, reccuring);
        return mapper.Map<IEnumerable<TaskViewModel>>(tasks);
    }
}
