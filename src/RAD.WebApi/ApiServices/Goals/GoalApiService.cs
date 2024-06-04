using AutoMapper;
using RAD.Domain.Entities;
using RAD.DTOs.Goals;
using RAD.Services.Configurations;
using RAD.Services.Services.Goals;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Goals;

namespace RAD.WebApi.ApiServices.Goals;

public class GoalApiService(
    IMapper mapper,
    IGoalService goalService,
    GoalCreateModelValidator createModelValidator,
    GoalUpdateModelValidator updateModelValidator) : IGoalApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await goalService.DeleteAsync(id);
    }

    public async ValueTask<GoalViewModel> GetAsync(long id)
    {
        var goal = await goalService.GetByIdAsync(id);
        return mapper.Map<GoalViewModel>(goal);
    }

    public async ValueTask<IEnumerable<GoalViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var goals = await goalService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<GoalViewModel>>(goals);
    }

    public async ValueTask<GoalViewModel> PostAsync(GoalCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var goal = await goalService.CreateAsync(mapper.Map<Goal>(model));
        return mapper.Map<GoalViewModel>(goal);
    }

    public async ValueTask<GoalViewModel> PutAsync(long id, GoalUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var goal = await goalService.UpdateAsync(id, mapper.Map<Goal>(model));
        return mapper.Map<GoalViewModel>(goal);
    }
}
