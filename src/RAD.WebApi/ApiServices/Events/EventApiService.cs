using AutoMapper;
using RAD.Domain.Entities;
using RAD.DTOs.Events;
using RAD.Services.Configurations;
using RAD.Services.Services.Events;
using RAD.WebApi.Extensions;
using RAD.WebApi.Validators.Events;

namespace RAD.WebApi.ApiServices.Events;

public class EventApiService(
    IMapper mapper,
    IEventService eventService,
    EventCreateModelValidator createModelValidator,
    EventUpdateModelValidator updateModelValidator) : IEventApiService
{
    public async ValueTask<EventViewModel> GetAsync(long id)
    {
        var @event = await eventService.GetByIdAsync(id);
        return mapper.Map<EventViewModel>(@event);
    }

    public async ValueTask<IEnumerable<EventViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var events = await eventService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<EventViewModel>>(events);
    }

    public async ValueTask<EventViewModel> PostAsync(EventCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var @event = await eventService.CreateAsync(mapper.Map<Event>(model));
        return mapper.Map<EventViewModel>(@event);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await eventService.DeleteAsync(id);
    }

    public async ValueTask<EventViewModel> PutAsync(long id, EventUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var @event = await eventService.UpdateAsync(id, mapper.Map<Event>(model));
        return mapper.Map<EventViewModel>(@event);
    }
}
