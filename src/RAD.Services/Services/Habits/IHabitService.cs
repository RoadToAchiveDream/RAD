using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Habits;

public interface IHabitService
{
    ValueTask<Habit> CreateAsync(Habit habit);
    ValueTask<Habit> UpdateAsync(long id, Habit habit);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Habit> GetByIdAsync(long id);
    ValueTask<IEnumerable<Habit>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
