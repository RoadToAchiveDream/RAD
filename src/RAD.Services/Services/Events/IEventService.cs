using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Events;

public interface IEventService
{
    ValueTask<Event> CreateAsync(Event @event);
    ValueTask<Event> UpdateAsync(long id, Event @event);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Event> GetByIdAsync(long id);
    ValueTask<IEnumerable<Event>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
