using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Habits;

public class HabitService(IUserService userService, IUnitOfWork unitOfWork) : IHabitService
{
    #region Habit CRUD
    public async ValueTask<Habit> CreateAsync(Habit habit)
    {
        var existUser = await userService.GetByIdAsync(habit.UserId);

        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Name == habit.Name && !h.IsDeleted);

        if (existHabit is not null)
            throw new AlreadyExistException($"Habit with name ({habit.Name} is already exists)");

        var created = await unitOfWork.Habits.InsertAsync(habit);
        await unitOfWork.SaveAsync();

        created.User = existUser;

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Habit with Id ({id}) is not found");

        await unitOfWork.Habits.DeleteAsync(existHabit);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Habit>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Habits = unitOfWork.Habits.SelectAsQueryable(
            expression: h => !h.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Habits = Habits.Where(habit =>
                habit.Name.ToLower().Contains(search.ToLower()) ||
                habit.Description.ToLower().Contains(search.ToLower()));

        return await Habits.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Habit> GetByIdAsync(long id)
    {
        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Id == id && !h.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Habit with Id ({id}) is not found");

        return existHabit;
    }

    public async ValueTask<Habit> UpdateAsync(long id, Habit habit)
    {
        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Habit with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(habit.UserId);

        existHabit.Name = habit.Name;
        existHabit.Description = habit.Description;
        existHabit.StartDate = habit.StartDate;
        existHabit.EndDate = habit.EndDate;
        existHabit.LastCompletedDate = habit.LastCompletedDate;
        existHabit.BestSteak = habit.BestSteak;
        existHabit.Steak = habit.Steak;
        existHabit.Frequency = habit.Frequency;
        existHabit.UserId = habit.UserId;
        existHabit.User = existUser;

        var updated = await unitOfWork.Habits.UpdateAsync(existHabit);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region Habit Features
    public ValueTask<Habit> SetEndTime(DateTime startTime)
    {
        throw new NotImplementedException();
    }
    public ValueTask<Habit> SetFrequency(string frequency)
    {
        throw new NotImplementedException();
    }
    public ValueTask<Habit> SetStartTime(DateTime startTime)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Habit> SetStreak(int streak)
    {
        throw new NotImplementedException();
    }
    public ValueTask<Habit> SetBestStreak(int bestStreak)
    {
        throw new NotImplementedException();
    }
    #endregion

}
