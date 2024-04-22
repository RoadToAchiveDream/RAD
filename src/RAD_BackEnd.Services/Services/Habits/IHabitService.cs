using RAD_BackEnd.DTOs.Habits;

namespace RAD_BackEnd.Services.Services.Habits;

public interface IHabitService
{
    ValueTask<HabitViewModel> CreateAsync(HabitCreateModel habit);
    ValueTask<HabitViewModel> UpdateAsync(long id, HabitUpdateModel habit);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<HabitViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<HabitViewModel>> GetAllAsync();
}
