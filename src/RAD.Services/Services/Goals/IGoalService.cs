using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Goals;

public interface IGoalService
{
    public ValueTask<Goal> CreateAsync(Goal goal);
    public ValueTask<Goal> UpdateAsync(long id, Goal goal);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Goal> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Goal>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
