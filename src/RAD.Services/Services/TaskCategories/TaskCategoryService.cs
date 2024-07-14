using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Tasks;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.TaskCategories;

public class TaskCategoryService(IUserService userService, ITaskService taskService, IUnitOfWork unitOfWork) : ITaskCategoryService
{
    #region TaskCategory CRUD
    public async ValueTask<TaskCategory> CreateAsync(TaskCategory taskCategory)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Name.ToLower() == taskCategory.Name.ToLower() && tc.UserId == HttpContextHelper.UserId)
            && !tc.IsDeleted);

        if (existTaskCategory is not null)
            throw new AlreadyExistException("Категория уже существует");

        taskCategory.UserId = existUser.Id;
        taskCategory.User = existUser;
        taskCategory.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.TaskCategories.InsertAsync(taskCategory);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Id == id && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted)
            ?? throw new NotFoundException($"Категория не найдена");

        existTaskCategory.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.TaskCategories.DeleteAsync(existTaskCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<TaskCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var taskCategories = unitOfWork.TaskCategories.SelectAsQueryable(
            expression: tc => !tc.IsDeleted && tc.UserId == HttpContextHelper.UserId,
            includes: ["Tasks"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            taskCategories = taskCategories.Where(tc =>
                tc.Name.ToLower().Contains(search.ToLower()));

        foreach (var item in taskCategories)
        {
            var filteredNotes = item.Tasks.Where(note => !note.IsDeleted).ToList();
            item.Tasks = filteredNotes;
        }

        return await taskCategories.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<TaskCategory> GetByIdAsync(long id)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Id == id && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted,
            includes: ["Tasks"])
            ?? throw new NotFoundException($"Категория не найдена");

        var filteredTasks = existTaskCategory.Tasks.Where(task => !task.IsDeleted);
        existTaskCategory.Tasks = filteredTasks;

        return existTaskCategory;
    }

    public async ValueTask<TaskCategory> UpdateAsync(long id, TaskCategory taskCategory)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
           expression: tc => (tc.Id == id && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted)
           ?? throw new NotFoundException($"Категория не найдена");

        existTaskCategory.Name = taskCategory.Name;
        existTaskCategory.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.TaskCategories.UpdateAsync(existTaskCategory);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region TaskCategory Features
    public async ValueTask<bool> AddTaskToCategoryAsync(long categoryId, long taskId)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Id == categoryId && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted)
            ?? throw new NotFoundException($"Категория не найдена");

        var set = await taskService.SetCategoryIdAsync(taskId, categoryId);

        return true;
    }
    public async ValueTask<bool> RemoveTaskFromCategoryAsync(long categoryId, long taskId)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Id == categoryId && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted)
            ?? throw new NotFoundException($"Категория не найдена");

        var unset = await taskService.UnsetCategoryIdAsync(taskId);

        return true;
    }
    public async ValueTask<TaskCategory> GetCategoryByNameAsync(string name)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Name.ToLower().Contains(name) && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted,
            includes: ["Tasks"])
            ?? throw new NotFoundException($"Категория не найдена");

        var filteredTasks = existCategory.Tasks.Where(note => !note.IsDeleted);
        existCategory.Tasks = filteredTasks;

        return existCategory;
    }
    #endregion
}
