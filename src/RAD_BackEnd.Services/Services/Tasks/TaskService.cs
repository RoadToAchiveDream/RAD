using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Extensions;
using RAD_BackEnd.Services.Services.Users;
using Task = RAD_BackEnd.Domain.Entities.Task;

namespace RAD_BackEnd.Services.Services.Tasks;

public class TaskService(IUserService userService, IUnitOfWork unitOfWork) : ITaskService
{
    public async ValueTask<Task> CreateAsync(Task task)
    {
        var existUser = await userService.GetByIdAsync(task.UserId);

        var createdTask = await unitOfWork.Tasks.InsertAsync(task);
        await unitOfWork.SaveAsync();

        createdTask.User = existUser;

        return createdTask;
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

    public async ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: t => !t.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Tasks = Tasks.Where(user =>
                user.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                user.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Task> GetByIdAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        return existsTask;
    }

    public async ValueTask<Task> UpdateAsync(long id, Task task)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(existsTask.UserId);

        existsTask.Status = task.Status;
        existsTask.Title = task.Title;
        existsTask.Description = task.Description;
        existsTask.DueDate = task.DueDate;
        existsTask.Priority = task.Priority;
        existsTask.Reccuring = task.Reccuring;
        existsTask.Reminder = task.Reminder;
        existsTask.UserId = task.UserId;
        existsTask.User = existUser;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
}
