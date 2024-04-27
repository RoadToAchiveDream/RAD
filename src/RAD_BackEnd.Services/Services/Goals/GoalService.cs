using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Goals;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Services.Users;

namespace RAD_BackEnd.Services.Services.Goals;

public class GoalService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : IGoalService
{
    public async ValueTask<GoalViewModel> CreateAsync(GoalCreateModel goal)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
            expression: g => g.Title == goal.Title && !g.IsDeleted);

        if (existGoal is not null)
            throw new AlreadyExistException($"Goal with Title ({goal.Title} is already exists)");

        var existsUser = await userService.GetByIdAsync(existGoal.UserId);

        var created = await unitOfWork.Goals.InsertAsync(mapper.Map<Goal>(goal));
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<GoalViewModel>(created);
        mapped.User = existsUser;

        return mapped;
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

    public async ValueTask<IEnumerable<GoalViewModel>> GetAllAsync()
    {
        var Goals = await unitOfWork.Goals.SelectAsEnumerableAsync(
            expression: g => !g.IsDeleted,
            includes: ["User"]);

        return mapper.Map<IEnumerable<GoalViewModel>>(Goals);
    }

    public async ValueTask<GoalViewModel> GetByIdAsync(long id)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
           expression: g => g.Id == id && !g.IsDeleted,
           includes: ["User"])
           ?? throw new NotFoundException($"Goal with Id ({id} is not found)");

        return mapper.Map<GoalViewModel>(existGoal);
    }

    public async ValueTask<GoalViewModel> UpdateAsync(long id, GoalUpdateModel goal)
    {
        var existGoal = await unitOfWork.Goals.SelectAsync(
           expression: g => g.Id == id && !g.IsDeleted)
           ?? throw new NotFoundException($"Goal with Id ({id} is not found)");

        var existUser = await userService.GetByIdAsync(goal.UserId);

        var mappedForUpdate = mapper.Map(goal, existGoal);
        var updated = await unitOfWork.Goals.UpdateAsync(mappedForUpdate);
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<GoalViewModel>(updated);
        mapped.User = existUser;

        return mapped;
    }
}
