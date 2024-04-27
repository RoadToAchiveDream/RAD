using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Events;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Services.Users;

namespace RAD_BackEnd.Services.Services.Events;

public class EventService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : IEventService
{
    public async ValueTask<EventViewModel> CreateAsync(EventCreateModel @event)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Title == @event.Title && !e.IsDeleted);

        if (existEvent is not null)
            throw new AlreadyExistException($"Event with Title ({@event.Title} is already exists)");

        var existUser = await userService.GetByIdAsync(@event.UserId);

        var created = await unitOfWork.Events.InsertAsync(mapper.Map<Event>(@event));
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<EventViewModel>(created);
        mapped.User = existUser;

        return mapped;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Event with Id ({id}) is not found");

        await unitOfWork.Events.DeleteAsync(existEvent);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<EventViewModel>> GetAllAsync()
    {
        var Events = await unitOfWork.Events.SelectAsEnumerableAsync(
            expression: e => !e.IsDeleted,
            includes: ["User"]);

        return mapper.Map<IEnumerable<EventViewModel>>(Events);
    }

    public async ValueTask<EventViewModel> GetByIdAsync(long id)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => !e.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Event with Id({id}) is not found");

        return mapper.Map<EventViewModel>(existEvent);
    }

    public async ValueTask<EventViewModel> UpdateAsync(long id, EventUpdateModel @event)
    {
        var existEvent = await unitOfWork.Events.SelectAsync(
            expression: e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Event with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(@event.UserId);

        var mappedForUpdate = mapper.Map(@event, existEvent);
        var updated = await unitOfWork.Events.UpdateAsync(mappedForUpdate);
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<EventViewModel>(updated);
        mapped.User = existUser;

        return mapped;
    }
}
