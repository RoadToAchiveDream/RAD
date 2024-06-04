using RAD.Domain.Entities;
using RAD.Services.Configurations;

namespace RAD.Services.Services.Notes;

public interface INoteService
{
    ValueTask<Note> CreateAsync(Note note);
    ValueTask<Note> UpdateAsync(long id, Note note);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Note> GetByIdAsync(long id);
    ValueTask<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
