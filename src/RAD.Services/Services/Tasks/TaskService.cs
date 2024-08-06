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
            ?? throw new NotFoundException($"Задача не найдена");

        existsTask.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Tasks.DeleteAsync(existsTask);
        await unitOfWork.SaveAsync();

        return true;
    }
    public async ValueTask<IEnumerable<Task>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: t => !t.IsDeleted && t.UserId == HttpContextHelper.UserId,
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
            expression: t => t.Id == id && !t.IsDeleted && t.UserId == HttpContextHelper.UserId)
            ?? throw new NotFoundException($"Задача не найдена");

        return existsTask;
    }
    public async ValueTask<Task> UpdateAsync(long id, Task task)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => (t.Id == id && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

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
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.DueDate == dueDate)
            throw new AlreadyExistException($"Для этой задачи уже установлен тот же срок выполнения.");

        existsTask.DueDate = dueDate;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetReminderAsync(long id, DateTime reminder)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Reminder == reminder)
            throw new AlreadyExistException("Для этой задачи уже установлено такое же напоминание.");

        existsTask.Reminder = reminder;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetStatusAsync(long id, string status)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Status.ToString() == status)
            throw new AlreadyExistException("Для этой задачи уже установлен такой же статус");

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
            default: throw new NotFoundException($"Этот статус ({status}) недействителен.");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetPriorityAsync(long id, string priority)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Priority.ToString() == priority)
            throw new AlreadyExistException("Для этой задачи уже установлен тот же приоритет.");

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
            default: throw new NotFoundException($"Этот приоритет ({priority}) недействителен.");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> SetReccuringAsync(long id, string reccuring)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Reccuring.ToString() == reccuring)
            throw new AlreadyExistException("Для этой задачи уже задана такая же повторяемость.");

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

            default: throw new NotFoundException($"Это повторение ({reccuring}) недействительно");
        }

        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<Task> UnsetDueDateAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.DueDate == DateTime.MinValue)
            throw new AlreadyExistException("Срок выполнения уже отменен или еще не установлен.");

        existsTask.DueDate = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReminderAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Reminder == DateTime.MinValue)
            throw new AlreadyExistException("Напоминание уже отключено или еще не установлено");

        existsTask.Reminder = DateTime.MinValue;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetStatusAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Status == TaskStatus.Pending)
            throw new AlreadyExistException("Статус уже отменен или еще не установлен");

        existsTask.Status = TaskStatus.Pending;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReccuringAsync(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Priority == TaskPriority.None)
            throw new AlreadyExistException("Приоритет уже отменен или еще не установлен");

        existsTask.Priority = TaskPriority.None;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetReccuring(long id)
    {
        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existsTask.Reccuring == TaskReccuring.None)
            throw new AlreadyExistException("Повторное выполнение уже отменено или еще не установлено.");

        existsTask.Reccuring = TaskReccuring.None;
        existsTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existsTask);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<IEnumerable<Task>> GetAllCompletedAsync(PaginationParams @params, Filter filter)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: t => !t.IsDeleted && t.IsCompleted && t.UserId == HttpContextHelper.UserId,
            isTracked: false).OrderBy(filter);

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetAllNotCompletedAsync(PaginationParams @params, Filter filter)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: t => !t.IsDeleted && !t.IsCompleted && t.UserId == HttpContextHelper.UserId,
            isTracked: false).OrderBy(filter);

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<IEnumerable<Task>> GetByDueDateAsync(PaginationParams @params, Filter filter, DateTime dueDate)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
             expression: task => (task.UserId == HttpContextHelper.UserId && task.DueDate == dueDate) && !task.IsDeleted,
             isTracked: false).OrderBy(filter)
             ?? throw new NotFoundException($"Задачи согласно этой дате выполнения ({dueDate}) не найдены.");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByReminderAsync(PaginationParams @params, Filter filter, DateTime reminder)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
             expression: task => (task.UserId == HttpContextHelper.UserId && task.Reminder == reminder) && !task.IsDeleted,
             isTracked: false).OrderBy(filter)
             ?? throw new NotFoundException($"Задачи по этому напоминанию ({reminder}) не найдены.");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByStatusAsync(PaginationParams @params, Filter filter, string status)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Status.ToString() == status) && !task.IsDeleted,
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Задачи по данному статусу ({status}) не найдены");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByPriorityAsync(PaginationParams @params, Filter filter, string priority)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Priority.ToString() == priority) && !task.IsDeleted,
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Задачи по данному приоритету ({priority}) не найдены.");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }
    public async ValueTask<IEnumerable<Task>> GetByReccuringAsync(PaginationParams @params, Filter filter, string reccuring)
    {
        var Tasks = unitOfWork.Tasks.SelectAsQueryable(
            expression: task => (task.UserId == HttpContextHelper.UserId && task.Reccuring.ToString() == reccuring) && !task.IsDeleted,
            isTracked: false).OrderBy(filter)
            ?? throw new NotFoundException($"Задачи по этому повторению ({reccuring}) не найдены.");

        return await Tasks.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Task> SetCategoryIdAsync(long taskId, long categoryId)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
             expression: t => t.Id == taskId && !t.IsDeleted)
             ?? throw new NotFoundException($"Задача не найдена");

        if (existTask.CategoryId == categoryId)
            throw new AlreadyExistException("Задача уже добавлена ​​в эту категорию");

        existTask.CategoryId = categoryId;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetCategoryIdAsync(long taskId)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
             expression: t => t.Id == taskId && !t.IsDeleted)
             ?? throw new NotFoundException($"Задача не найдена");

        if (existTask.CategoryId == 0 || existTask.CategoryId is null)
            throw new AlreadyExistException("Задача еще не добавлена ни к одной категории");

        existTask.CategoryId = null;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }

    public async ValueTask<Task> SetIsCompletedAsync(long id)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existTask.IsCompleted == true)
            throw new AlreadyExistException("Задача уже выполнена");

        existTask.IsCompleted = true;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Task> UnsetIsCompletedAsync(long id)
    {
        var existTask = await unitOfWork.Tasks.SelectAsync(
            expression: task => (task.Id == id && task.UserId == HttpContextHelper.UserId) && !task.IsDeleted)
            ?? throw new NotFoundException($"Задача не найдена");

        if (existTask.IsCompleted == false)
            throw new AlreadyExistException("Задача уже находится в статусе не выполнено");

        existTask.IsCompleted = false;
        existTask.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Tasks.UpdateAsync(existTask);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion
}
