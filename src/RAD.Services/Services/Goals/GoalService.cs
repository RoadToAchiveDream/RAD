using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Goals;

public class GoalService(IUserService userService, IUnitOfWork unitOfWork) : IGoalService
{
    #region Goal CRUD
    public async ValueTask<Goal> CreateAsync(Goal goal)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
            expression: g => g.Title == goal.Title && !g.IsDeleted);

        if (existGoal is not null)
            throw new AlreadyExistException($"Goal with Title ({goal.Title} is already exists)");

        var existsUser = await userService.GetByIdAsync(goal.UserId);

        var created = await unitOfWork.Goals.InsertAsync(goal);
        await unitOfWork.SaveAsync();

        created.User = existsUser;

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
            expression: g => g.Id == id && !g.IsDeleted)
            ?? throw new NotFoundException($"Goal with Id ({id} is not found)");

        await unitOfWork.Goals.DeleteAsync(existGoal);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Goal>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Goals = unitOfWork.Goals.SelectAsQueryable(
            expression: g => !g.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Goals = Goals.Where(goal =>
            goal.Title.ToLower().Contains(search.ToLower()) ||
            goal.Description.ToLower().Contains(search.ToLower()));

        return await Goals.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Goal> GetByIdAsync(long id)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
           expression: g => g.Id == id && !g.IsDeleted,
           includes: ["User"])
           ?? throw new NotFoundException($"Goal with Id ({id} is not found)");

        return existGoal;
    }

    public async ValueTask<Goal> UpdateAsync(long id, Goal goal)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
           expression: g => g.Id == id && !g.IsDeleted)
           ?? throw new NotFoundException($"Goal with Id ({id} is not found)");

        var existUser = await userService.GetByIdAsync(goal.UserId);

        existGoal.Title = goal.Title;
        existGoal.Description = goal.Description;
        existGoal.StartTime = goal.StartTime;
        existGoal.EndTime = goal.EndTime;
        existGoal.Status = goal.Status;
        existGoal.Progress = goal.Progress;
        existGoal.UserId = goal.UserId;
        existGoal.User = existUser;

        var updated = await unitOfWork.Goals.UpdateAsync(existGoal);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion
}
