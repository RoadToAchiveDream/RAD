using RAD_BackEnd.Domain.Entities;

namespace RAD_BackEnd.Services.Services.Notes;

public interface INoteService
{
    ValueTask<Note> CreateAsync(Note note);
    ValueTask<Note> UpdateAsync(long id, Note note);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Note> GetByIdAsync(long id);
    ValueTask<IEnumerable<Note>> GetAllAsync();
}
