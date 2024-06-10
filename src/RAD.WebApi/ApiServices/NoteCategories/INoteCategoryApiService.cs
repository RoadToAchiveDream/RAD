using RAD.Services.Configurations;
using RAD.WebApi.DTOs.NoteCategories;

namespace RAD.WebApi.ApiServices.NoteCategories;

public interface INoteCategoryApiService
{
    public ValueTask<NoteCategoryViewModel> PostAsync(NoteCategoryCreateModel model);
    public ValueTask<NoteCategoryViewModel> PutAsync(long id, NoteCategoryUpdateModel model);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<NoteCategoryViewModel> GetAsync(long id);
    public ValueTask<IEnumerable<NoteCategoryViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
