using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Habits;

public interface IHabitService
{
    public ValueTask<Habit> CreateAsync(Habit habit);
    public ValueTask<Habit> UpdateAsync(long id, Habit habit);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Habit> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Habit>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<Habit> SetStartTime(DateTime startTime);
    public ValueTask<Habit> SetEndTime(DateTime startTime);
    public ValueTask<Habit> SetFrequency(string frequency);
    public ValueTask<Habit> SetStreak(int streak);
    public ValueTask<Habit> SetBestStreak(int bestStreak);
}
