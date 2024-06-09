using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Events;

public interface IEventService
{
    public ValueTask<Event> CreateAsync(Event @event);
    public ValueTask<Event> UpdateAsync(long id, Event @event);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Event> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Event>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
