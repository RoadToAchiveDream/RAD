using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Events;

public class EventService(IUserService userService, IUnitOfWork unitOfWork) : IEventService
{
    #region Event CRUD
    public async ValueTask<Event> CreateAsync(Event @event)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Title == @event.Title && !e.IsDeleted);

        if (existEvent is not null)
            throw new AlreadyExistException($"Event with Title ({@event.Title} is already exists)");

        @event.UserId = existUser.Id;
        @event.User = existUser;
        @event.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.Events.InsertAsync(@event);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Event with Id ({id}) is not found");

        existEvent.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Events.DeleteAsync(existEvent);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Event>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Events = unitOfWork.Events.SelectAsQueryable(
            expression: e => !e.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Events = Events.Where(@event =>
                @event.Title.ToLower().Contains(search.ToLower()) ||
                @event.Description.ToLower().Contains(search.ToLower()));

        return await Events.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Event> GetByIdAsync(long id)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Id == id && !e.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Event with Id({id}) is not found");

        return existEvent;
    }

    public async ValueTask<Event> UpdateAsync(long id, Event @event)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Event with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(@event.UserId);

        existEvent.UserId = @event.UserId;
        existEvent.User = existUser;
        existEvent.StartTime = @event.StartTime;
        existEvent.EndTime = @event.EndTime;
        existEvent.Title = @event.Title;
        existEvent.Description = @event.Description;
        existEvent.Location = @event.Location;
        existEvent.Reminder = @event.Reminder;
        existEvent.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Events.UpdateAsync(existEvent);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion
}
