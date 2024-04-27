using RAD_BackEnd.DTOs.Goals;

namespace RAD_BackEnd.Services.Services.Goals;

public interface IGoalService
{
    ValueTask<GoalViewModel> CreateAsync(GoalCreateModel goal);
    ValueTask<GoalViewModel> UpdateAsync(long id, GoalUpdateModel goal);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<GoalViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<GoalViewModel>> GetAllAsync();
}
