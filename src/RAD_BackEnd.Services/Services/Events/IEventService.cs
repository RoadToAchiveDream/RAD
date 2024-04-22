using RAD_BackEnd.DTOs.Events;

namespace RAD_BackEnd.Services.Services.Events;

public interface IEventService
{
    ValueTask<EventViewModel> CreateAsync(EventCreateModel @event);
    ValueTask<EventViewModel> UpdateAsync(long id, EventUpdateModel @event);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<EventViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<EventViewModel>> GetAllAsync();
}
