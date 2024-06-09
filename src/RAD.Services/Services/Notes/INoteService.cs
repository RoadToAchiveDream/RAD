using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Notes;

public interface INoteService
{
    public ValueTask<Note> CreateAsync(Note note);
    public ValueTask<Note> UpdateAsync(long id, Note note);
    public ValueTask<bool> DeleteAsync(long id);
    public ValueTask<Note> GetByIdAsync(long id);
    public ValueTask<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    public ValueTask<Note> SetPinned(long id);
    public ValueTask<Note> UnsetPinned(long id);
}
