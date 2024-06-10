﻿using Microsoft.Extensions.Options;
using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.TaskCategories;

public interface ITaskCategoryService
{
    public ValueTask<TaskCategory> CreateAsync(TaskCategory taskCategory);
    public ValueTask<TaskCategory> UpdateAsync(long id, TaskCategory taskCategory);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<TaskCategory> GetByIdAsync(long id);
    public ValueTask<IEnumerable<TaskCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<TaskCategory> AddTaskToCategoryAsync(long categoryId, long taskId);
    public ValueTask<TaskCategory> RemoveTaskFromCategoryAsync(long categoryId, long taskId);
    public ValueTask<TaskCategory> GetCategoryByNameAsync(string name);
}
