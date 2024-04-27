using AutoMapper;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.DTOs.Notes;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Services.Users;

namespace RAD_BackEnd.Services.Services.Notes;

public class NoteService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper) : INoteService
{
    public async ValueTask<NoteViewModel> CreateAsync(NoteCreateModel note)
    {
        var existUser = await userService.GetByIdAsync(note.UserId);

        var created = await unitOfWork.Notes.InsertAsync(mapper.Map<Note>(note));
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<NoteViewModel>(note);
        mapped.User = existUser;

        return mapped;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => n.Id == id && !n.IsDeleted)
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        await unitOfWork.Notes.DeleteAsync(existNote);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<NoteViewModel>> GetAllAsync()
    {
        var Notes = await unitOfWork.Notes.SelectAsEnumerableAsync(
            expression: n => !n.IsDeleted,
            includes: ["User"]);

        return mapper.Map<IEnumerable<NoteViewModel>>(Notes);
    }

    public async ValueTask<NoteViewModel> GetByIdAsync(long id)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => n.Id == id && !n.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        return mapper.Map<NoteViewModel>(existNote);
    }

    public async ValueTask<NoteViewModel> UpdateAsync(long id, NoteUpdateModel note)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => n.Id == id && !n.IsDeleted)
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(note.UserId);

        var mappedForUpdate = mapper.Map(note, existNote);
        var updated = await unitOfWork.Notes.UpdateAsync(mappedForUpdate);
        await unitOfWork.SaveAsync();

        var mapped = mapper.Map<NoteViewModel>(updated);
        mapped.User = existUser;

        return mapped;
    }
}
