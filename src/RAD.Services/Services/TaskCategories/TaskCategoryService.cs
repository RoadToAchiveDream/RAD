using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;

namespace RAD.Services.Services.TaskCategories;

public class TaskCategoryService(IUnitOfWork unitOfWork) : ITaskCategoryService
{
    #region TaskCategory CRUD
    public async ValueTask<TaskCategory> CreateAsync(TaskCategory taskCategory)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => tc.Name.ToLower() == taskCategory.Name.ToLower() && !tc.IsDeleted);

        if (existTaskCategory is not null)
            throw new AlreadyExistException("Category with this name is already exists");

        taskCategory.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.TaskCategories.InsertAsync(taskCategory);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => tc.Id == id && !tc.IsDeleted)
            ?? throw new NotFoundException($"Category with Id ({id}) is not found");

        existTaskCategory.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.TaskCategories.DeleteAsync(existTaskCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<TaskCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var taskCategories = unitOfWork.TaskCategories.SelectAsQueryable(
            expression: tc => !tc.IsDeleted,
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            taskCategories = taskCategories.Where(tc =>
                tc.Name.ToLower().Contains(search.ToLower()));

        return await taskCategories.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<TaskCategory> GetByIdAsync(long id)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => tc.Id == id && !tc.IsDeleted)
            ?? throw new NotFoundException($"category with Id ({id}) is not found");

        return existTaskCategory;
    }

    public async ValueTask<TaskCategory> UpdateAsync(long id, TaskCategory taskCategory)
    {
        var existTaskCategory = await unitOfWork.TaskCategories.SelectAsync(
           expression: tc => tc.Id == id && !tc.IsDeleted)
           ?? throw new NotFoundException($"category with Id ({id}) is not found");

        existTaskCategory.Name = taskCategory.Name;
        existTaskCategory.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.TaskCategories.UpdateAsync(existTaskCategory);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region TaskCategory Features
    public async ValueTask<TaskCategory> AddTaskToCategory(long categoryId, long taskId)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Id == categoryId && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted,
            includes: ["User", "Tasks"])
            ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => (t.Id == taskId && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted,
            includes: ["User", "Category"])
            ?? throw new NotFoundException($"Task with Id ({taskId}) is not found");

        existsTask.CategoryId = categoryId;
        existsTask.Category = existCategory;
        await unitOfWork.SaveAsync();

        return await GetByIdAsync(categoryId);
    }
    public async ValueTask<TaskCategory> RemoveTaskFromCategory(long categoryId, long taskId)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
           expression: tc => (tc.Id == categoryId && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted,
           includes: ["User", "Tasks"])
           ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var existsTask = await unitOfWork.Tasks.SelectAsync(
            expression: t => (t.Id == taskId && t.UserId == HttpContextHelper.UserId) && !t.IsDeleted,
            includes: ["User", "Category"])
            ?? throw new NotFoundException($"Task with Id ({taskId}) is not found");

        existsTask.CategoryId = 0;
        existsTask.Category = null;
        await unitOfWork.SaveAsync();

        return await GetByIdAsync(categoryId);
    }
    public async ValueTask<TaskCategory> GetCategoryByName(string name)
    {
        var existCategory = await unitOfWork.TaskCategories.SelectAsync(
            expression: tc => (tc.Name.ToLower().Contains(name) && tc.UserId == HttpContextHelper.UserId) && !tc.IsDeleted,
            includes: ["User", "Tasks"])
            ?? throw new NotFoundException($"Category with name ({name}) is not found");

        return existCategory;
    }
    #endregion
}
