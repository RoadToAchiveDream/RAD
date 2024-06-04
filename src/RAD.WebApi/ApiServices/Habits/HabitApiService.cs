using AutoMapper;
using RAD.Domain.Entities;
using RAD.DTOs.Habits;
using RAD.Services.Configurations;
using RAD.Services.Services.Habits;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Habits;

namespace RAD.WebApi.ApiServices.Habits;

public class HabitApiService(
    IMapper mapper,
    IHabitService habitService,
    HabitCreateModelValidator createModelValidator,
    HabitUpdateModelValidator updateModelValidator) : IHabitApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await habitService.DeleteAsync(id);
    }

    public async ValueTask<HabitViewModel> GetAsync(long id)
    {
        var habit = await habitService.GetByIdAsync(id);
        return mapper.Map<HabitViewModel>(habit);
    }

    public async ValueTask<IEnumerable<HabitViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var habits = await habitService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<HabitViewModel>>(habits);
    }

    public async ValueTask<HabitViewModel> PostAsync(HabitCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var habit = await habitService.CreateAsync(mapper.Map<Habit>(model));
        return mapper.Map<HabitViewModel>(habit);
    }

    public async ValueTask<HabitViewModel> PutAsync(long id, HabitUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var habit = await habitService.UpdateAsync(id, mapper.Map<Habit>(model));
        return mapper.Map<HabitViewModel>(habit);
    }
}
