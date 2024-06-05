using RAD.Services.Configurations;
using RAD.WebApi.DTOs.Goals;

namespace RAD.WebApi.ApiServices.Goals;

public interface IGoalApiService
{
    public ValueTask<GoalViewModel> PostAsync(GoalCreateModel model);
    public ValueTask<GoalViewModel> PutAsync(long id, GoalUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<GoalViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<GoalViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
