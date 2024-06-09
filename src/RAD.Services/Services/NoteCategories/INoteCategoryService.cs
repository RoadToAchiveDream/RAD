using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.NoteCategories;

public interface INoteCategoryService
{
    public ValueTask<NoteCategory> CreateAsync(NoteCategory noteCategory);
    public ValueTask<NoteCategory> UpdateAsync(long id, NoteCategory noteCategory);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<NoteCategory> GetByIdAsync(long id);
    public ValueTask<IEnumerable<NoteCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
