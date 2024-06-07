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
                user.Title.ToLower().Contains(search.ToLower()) ||
                user.Description.ToLower().Contains(search.ToLower()));

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

        existsTask.Title = task.Title;
        existsTask.Description = task.Description;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region Task Features
    public async ValueTask<Task> SetDueDate(long id, DateTime dueDate)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.DueDate == dueDate)
            throw new AlreadyExistException($"Same DueDate is already set for this task");

        existsTask.DueDate = dueDate;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> SetReminder(long id, DateTime reminder)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reminder == reminder)
            throw new AlreadyExistException("Same Reminder is already set for this task");

        existsTask.Reminder = reminder;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> SetStatus(long id, string status)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
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
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> SetPriority(long id, string priority)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
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
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> SetReccuring(long id, string reccuring)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
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
        await unitOfWork.SaveAsync();

        return existsTask;
    }

    public async ValueTask<Task> UnsetDueDate(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.DueDate == DateTime.MinValue)
            throw new AlreadyExistException("Already unset or not set DueDate yet");

        existsTask.DueDate = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> UnsetReminder(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reminder == DateTime.MinValue)
            throw new AlreadyExistException("Already unset of not set Reminder yet");

        existsTask.Reminder = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> UnsetStatus(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Status == TaskStatus.Pending)
            throw new AlreadyExistException("Already unset or not set Status yet");

        existsTask.Status = TaskStatus.Pending;

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> UnsetPriority(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Priority == TaskPriority.None)
            throw new AlreadyExistException("Already unset or not set Priority yet");

        existsTask.Priority = TaskPriority.None;

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    public async ValueTask<Task> UnsetReccuring(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => task.Id == id && !task.IsDeleted,
            includes: ["Users"])
            ?? throw new NotFoundException($"Task with Id ({id}) is not found");

        if (existsTask.Reccuring == TaskReccuring.None)
            throw new AlreadyExistException("Already unset or not set Reccuring yet");

        existsTask.Reccuring = TaskReccuring.None;

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsTask;
    }
    #endregion
}
