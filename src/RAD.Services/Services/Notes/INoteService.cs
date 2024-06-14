using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Notes;

public interface INoteService
{
    #region Note CRUD
    public ValueTask<Note> CreateAsync(Note note);
    public ValueTask<Note> UpdateAsync(long id, Note note);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Note> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    #endregion

    #region Note Features
    public ValueTask<Note> SetPinned(long id);
    public ValueTask<Note> UnsetPinned(long id);

    public ValueTask<Note> SetCategoryId(long noteId, long categoryId);
    public ValueTask<Note> UnsetCategoryId(long noteId);
    #endregion
}
