using RAD.DTOs.Events;
using RAD.Services.Configurations;

namespace RAD.WebApi.ApiServices.Events;

public interface IEventApiService
{
    public ValueTask<EventViewModel> PostAsync(EventCreateModel model);
    public ValueTask<EventViewModel> PutAsync(long id, EventUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<EventViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<EventViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
