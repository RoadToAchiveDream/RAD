using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.DTOs.Tasks;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Services.Users;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.Services.Services.Tasks;

public class TaskService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : ITaskService
{
    public async ValueTask<TaskViewModel> CreateAsync(TaskCreateModel task)
    {
        var existUser = await userService.GetByIdAsync(task.UserId);

        var createdTask = await unitOfWork.Tasks.InsertAsync(mapper.Map<Task>(task));
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<TaskViewModel>(createdTask);
        mapped.User = existUser;

        return mapped;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        await unitOfWork.Tasks.DeleteAsync(existsTask);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<TaskViewModel>> GetAllAsync()
    {
        var Tasks = await unitOfWork.Tasks.SelectAsEnumerableAsync(
            expression: t => !t.IsDeleted,
            includes: ["User"]);

        return mapper.Map<IEnumerable<TaskViewModel>>(Tasks);
    }

    public async ValueTask<TaskViewModel> GetByIdAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(existsTask.UserId);

        var mapped = mapper.Map<TaskViewModel>(existsTask);
        mapped.User = existUser;

        return mapped;
    }

    public async ValueTask<TaskViewModel> UpdateAsync(long id, TaskUpdateModel task)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(existsTask.UserId);

        var mappedForUpdate = mapper.Map(task, existsTask);
        var updated = await unitOfWork.Tasks.UpdateAsync(mappedForUpdate);
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<TaskViewModel>(updated);
        mapped.User = existUser;

        return mapped;
    }
}
