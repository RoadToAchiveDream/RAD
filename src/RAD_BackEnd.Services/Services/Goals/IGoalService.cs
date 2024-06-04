using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.Services.Configurations;

namespace RAD_BackEnd.Services.Services.Goals;

public interface IGoalService
{
    ValueTask<Goal> CreateAsync(Goal goal);
    ValueTask<Goal> UpdateAsync(long id, Goal goal);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Goal> GetByIdAsync(long id);
    ValueTask<IEnumerable<Goal>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
