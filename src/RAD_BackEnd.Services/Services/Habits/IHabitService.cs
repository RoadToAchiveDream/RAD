using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.Services.Configurations;

namespace RAD_BackEnd.Services.Services.Habits;

public interface IHabitService
{
    ValueTask<Habit> CreateAsync(Habit habit);
    ValueTask<Habit> UpdateAsync(long id, Habit habit);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Habit> GetByIdAsync(long id);
    ValueTask<IEnumerable<Habit>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
