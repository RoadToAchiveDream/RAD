using RAD.DTOs.Habits;
using RAD.Services.Configurations;

namespace RAD.WebApi.ApiServices.Habits;

public interface IHabitApiService
{
    public ValueTask<HabitViewModel> PostAsync(HabitCreateModel model);
    public ValueTask<HabitViewModel> PutAsync(long id, HabitUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<HabitViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<HabitViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
