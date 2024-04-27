using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Habits;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Services.Users;

namespace RAD_BackEnd.Services.Services.Habits;

public class HabitService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : IHabitService
{
    public async ValueTask<HabitViewModel> CreateAsync(HabitCreateModel habit)
    {
        var existUser = await userService.GetByIdAsync(habit.UserId);

        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Name == habit.Name && !h.IsDeleted);

        if (existHabit is not null)
            throw new AlreadyExistException($"Habit with name ({habit.Name} is already exists)");

        var created = await unitOfWork.Habits.InsertAsync(mapper.Map<Habit>(habit));
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<HabitViewModel>(created);
        mapped.User = existUser;

        return mapped;
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

    public async ValueTask<IEnumerable<HabitViewModel>> GetAllAsync()
    {
        var Habits = await unitOfWork.Habits.SelectAsEnumerableAsync(
            expression: h => !h.IsDeleted,
            includes: ["User"]);

        return mapper.Map<IEnumerable<HabitViewModel>>(Habits);
    }

    public async ValueTask<HabitViewModel> GetByIdAsync(long id)
    {
        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Id == id && !h.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Habit with Id ({id}) is not found");

        return mapper.Map<HabitViewModel>(existHabit);
    }

    public async ValueTask<HabitViewModel> UpdateAsync(long id, HabitUpdateModel habit)
    {
        var existHabit = await unitOfWork.Habits.SelectAsync(
            expression: h => h.Id == id && !h.IsDeleted)
            ?? throw new NotFoundException($"Habit with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(habit.UserId);

        var mappedForUpdate = mapper.Map(habit, existHabit);
        var updated = await unitOfWork.Habits.UpdateAsync(mappedForUpdate);
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<HabitViewModel>(updated);
        mapped.User = existUser;

        return mapped;
    }
}
