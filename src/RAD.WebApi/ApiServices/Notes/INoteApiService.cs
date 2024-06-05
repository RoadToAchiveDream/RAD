using RAD.Services.Configurations;
using RAD.WebApi.DTOs.Notes;

namespace RAD.WebApi.ApiServices.Notes;

public interface INoteApiService
{
    public ValueTask<NoteViewModel> PostAsync(NoteCreateModel model);
    public ValueTask<NoteViewModel> PutAsync(long id, NoteUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<NoteViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<NoteViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
