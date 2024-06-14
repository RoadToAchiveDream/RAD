using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.NoteCategories;

public interface INoteCategoryService
{
    #region NoteCategory CRUD
    public ValueTask<NoteCategory> CreateAsync(NoteCategory noteCategory);
    public ValueTask<NoteCategory> UpdateAsync(long id, NoteCategory noteCategory);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<NoteCategory> GetByIdAsync(long id);
    public ValueTask<IEnumerable<NoteCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    #endregion

    #region NoteCategory Features
    public ValueTask<bool> AddNoteToCategoryAsync(long categoryId, long noteId);
    public ValueTask<bool> RemoveNoteFromCategoryAsync(long categoryId, long noteId);
    public ValueTask<NoteCategory> GetCategoryByNameAsync(string name);
    #endregion
}
