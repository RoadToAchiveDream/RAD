using RAD_BackEnd.DTOs.Notes;

namespace RAD_BackEnd.Services.Services.Notes;

public interface INoteService
{
    ValueTask<NoteViewModel> CreateAsync(NoteCreateModel note);
    ValueTask<NoteViewModel> UpdateAsync(long id, NoteUpdateModel note);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<NoteViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<NoteViewModel>> GetAllAsync();
}
