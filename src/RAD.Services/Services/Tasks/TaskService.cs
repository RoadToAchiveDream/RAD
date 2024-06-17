using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Enums.TaskEnums;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Users;
using Task = RAD.Domain.Entities.Task;
using TaskStatus = RAD.Domain.Enums.TaskEnums.TaskStatus;

namespace RAD.Services.Services.Tasks;

public class TaskService(IUserService userService, IUnitOfWork unitOfWork) : ITaskService
{
    #region Task CRUD
    public async ValueTask<Task> CreateAsync(Task task)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        task.UserId = existUser.Id;
        task.User = existUser;
        task.CreatedByUserId = HttpContextHelper.UserId;

        var createdTask = await unitOfWork.Tasks.InsertAsync(task);
        await unitOfWork.SaveAsync();

        return createdTask;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => (t.Id == id && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        existsTask.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Tasks.DeleteAsync(existsTask);
        await unitOfWork.SaveAsync();

        return true;
    }
    public async ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: t => !t.IsDeleted && t.UserId == HttpContextHelper.UserId,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Tasks = Tasks.Where(user =>
                user.Title.ToLower().Contains(search.ToLower()) ||
                user.Description.ToLower().Contains(search.ToLower()));

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<Task> GetByIdAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => t.Id == id && !t.IsDeleted && t.UserId == HttpContextHelper.UserId,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        return existsTask;
    }
    public async ValueTask<Task> UpdateAsync(long id, Task task)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => (t.Id == id && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted)
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(existsTask.UserId);

        existsTask.Title = task.Title;
        existsTask.Description = task.Description;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region Task Features
    public async ValueTask<Task> SetDueDateAsync(long id, DateTime dueDate)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.DueDate == dueDate)
            throw new AlreadyExistException($"Same DueDate is already set for this task");

        existsTask.DueDate = dueDate;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetReminderAsync(long id, DateTime reminder)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reminder == reminder)
            throw new AlreadyExistException("Same Reminder is already set for this task");

        existsTask.Reminder = reminder;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetStatusAsync(long id, string status)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Status.ToString() == status)
            throw new AlreadyExistException("Same status is already set for this task");

        switch (status)
        {
            case "Pending":
                existsTask.Status = TaskStatus.Pending;
                break;
            case "InProcess":
                existsTask.Status = TaskStatus.InProgress;
                break;
            case "Cancelled":
                existsTask.Status = TaskStatus.Cancelled;
                break;
            case "Completed":
                existsTask.Status = TaskStatus.Completed;
                break;
            default: throw new NotFoundException($"this status ({status}) is not valid");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetPriorityAsync(long id, string priority)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Priority.ToString() == priority)
            throw new AlreadyExistException("Same Priority is already set for this task");

        switch (priority)
        {
            case "Low":
                existsTask.Priority = TaskPriority.Low;
                break;
            case "Medium":
                existsTask.Priority = TaskPriority.Medium;
                break;
            case "High":
                existsTask.Priority = TaskPriority.High;
                break;
            default: throw new NotFoundException($"this priority ({priority}) is not valid");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetReccuringAsync(long id, string reccuring)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reccuring.ToString() == reccuring)
            throw new AlreadyExistException("Same Reccuring is already set for this task");

        switch (reccuring)
        {
            case "Daily":
                existsTask.Reccuring = TaskReccuring.Daily;
                break;
            case "Weekly":
                existsTask.Reccuring = TaskReccuring.Weekly;
                break;
            case "Monthly":
                existsTask.Reccuring = TaskReccuring.Monthly;
                break;
            case "Yearly":
                existsTask.Reccuring = TaskReccuring.Yearly;
                break;

            default: throw new NotFoundException($"this reccuring ({reccuring}) is not valid");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<Task> UnsetDueDateAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.DueDate == DateTime.MinValue)
            throw new AlreadyExistException("Already unset or not set DueDate yet");

        existsTask.DueDate = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReminderAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reminder == DateTime.MinValue)
            throw new AlreadyExistException("Already unset of not set Reminder yet");

        existsTask.Reminder = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetStatusAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Status == TaskStatus.Pending)
            throw new AlreadyExistException("Already unset or not set Status yet");

        existsTask.Status = TaskStatus.Pending;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReccuringAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Priority == TaskPriority.None)
            throw new AlreadyExistException("Already unset or not set Priority yet");

        existsTask.Priority = TaskPriority.None;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReccuring(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reccuring == TaskReccuring.None)
            throw new AlreadyExistException("Already unset or not set Reccuring yet");

        existsTask.Reccuring = TaskReccuring.None;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<IEnumerable<Task>> GetByDueDateAsync(PaginationParams @params, Filter filter, DateTime dueDate)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
             expression: task => (task.UserId == HttpContextHelper.UserId && task.DueDate == dueDate) && !task.IsDeleted,
             includes: ["User"],
             isTracked: false).OrderBy(filter)
             ?? throw new NotFoundException($"Tasks according to this dueDate ({dueDate}) are not found");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByReminderAsync(PaginationParams @params, Filter filter, DateTime reminder)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
             expression: task => (task.UserId == HttpContextHelper.UserId && task.Reminder == reminder) && !task.IsDeleted,
             includes: ["User"],
             isTracked: false).OrderBy(filter)
             ?? throw new NotFoundException($"Tasks according to this reminder ({reminder}) are not found");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByStatusAsync(PaginationParams @params, Filter filter, string status)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Status.ToString() == status) && !task.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Tasks according to this status ({status}) are not found");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByPriorityAsync(PaginationParams @params, Filter filter, string priority)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Priority.ToString() == priority) && !task.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Tasks according to this priority ({priority}) are not found");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByReccuringAsync(PaginationParams @params, Filter filter, string reccuring)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Reccuring.ToString() == reccuring) && !task.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Tasks according to this reccuring ({reccuring}) are not found");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Task> SetCategoryId(long taskId, long categoryId)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
             expression: t => t.Id == taskId && !t.IsDeleted)
             ?? throw new NotFoundException($"Task with Id ({taskId}) is not found");

        if (existTask.CategoryId == categoryId)
            throw new AlreadyExistException("Task is already set to this category");

        existTask.CategoryId = categoryId;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetCategoryId(long taskId)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
             expression: t => t.Id == taskId && !t.IsDeleted)
             ?? throw new NotFoundException($"Task with Id ({taskId}) is not found");

        if (existTask.CategoryId == 0 || existTask.CategoryId is null)
            throw new ArgumentIsNotValidException("Task has not set to any category");

        existTask.CategoryId = null;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion
}
