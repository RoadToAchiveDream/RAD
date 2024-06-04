using Microsoft.EntityFrameworkCore;
using RAD_BackEnd.DataAccess.UnintOfWorks;
using RAD_BackEnd.Domain.Entities;
using RAD_BackEnd.Services.Configurations;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.Services.Extensions;
using RAD_BackEnd.Services.Services.Users;

namespace RAD_BackEnd.Services.Services.Notes;

public class NoteService(IUserService userService, IUnitOfWork unitOfWork) : INoteService
{
    public async ValueTask<Note> CreateAsync(Note note)
    {
        var existUser = await userService.GetByIdAsync(note.UserId);

        var created = await unitOfWork.Notes.InsertAsync(note);
        await unitOfWork.SaveAsync();

        created.User = existUser;

        return created;
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

    public async ValueTask<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Notes = unitOfWork.Notes.SelectAsQueryable(
            expression: n => !n.IsDeleted,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Notes = Notes.Where(user =>
                user.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                user.Content.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await Notes.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Note> GetByIdAsync(long id)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => n.Id == id && !n.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        return existNote;
    }

    public async ValueTask<Note> UpdateAsync(long id, Note note)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => n.Id == id && !n.IsDeleted)
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(note.UserId);

        existNote.Title = note.Title;
        existNote.Content = note.Content;
        existNote.Category = note.Category;
        existNote.UserId = note.UserId;
        existNote.User = existUser;

        var updated = await unitOfWork.Notes.UpdateAsync(existNote);
        await unitOfWork.SaveAsync();

        return updated;
    }
}
