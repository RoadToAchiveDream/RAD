using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.Notes;

public class NoteService(IUserService userService, IUnitOfWork unitOfWork) : INoteService
{
    #region Note CRUD
    public async ValueTask<Note> CreateAsync(Note note)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        note.UserId = existUser.Id;
        note.User = existUser;
        note.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.Notes.InsertAsync(note);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => (n.Id == id && n.UserId == HttpContextHelper.UserId) && !n.IsDeleted)
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        existNote.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Notes.DeleteAsync(existNote);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var Notes = unitOfWork.Notes.SelectAsQueryable(
            expression: n => !n.IsDeleted && n.UserId == HttpContextHelper.UserId,
            includes: ["User"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            Notes = Notes.Where(user =>
                user.Title.ToLower().Contains(search.ToLower()) ||
                user.Content.ToLower().Contains(search.ToLower()));

        return await Notes.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Note> GetByIdAsync(long id)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => (n.Id == id && n.UserId == HttpContextHelper.UserId) && !n.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        return existNote;
    }

    public async ValueTask<Note> UpdateAsync(long id, Note note)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
            expression: n => (n.Id == id && n.UserId == HttpContextHelper.UserId) && !n.IsDeleted)
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        existNote.Title = note.Title;
        existNote.Content = note.Content;
        existNote.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Notes.UpdateAsync(existNote);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region Note Feateures
    public async ValueTask<Note> SetPinned(long id)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
            expression: note => (note.Id == id && note.UserId == HttpContextHelper.UserId) && !note.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        if (existsNote.IsPinned)
            throw new AlreadyExistException("Note is already pinned");

        existsNote.IsPinned = true;
        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }
    public async ValueTask<Note> UnsetPinned(long id)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
           expression: note => (note.Id == id && note.UserId == HttpContextHelper.UserId) && !note.IsDeleted,
           includes: ["User"])
           ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        if (!existsNote.IsPinned)
            throw new AlreadyExistException("Note is already unpinned");

        existsNote.IsPinned = false;
        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }

    public async ValueTask<Note> SetCategoryId(long noteId, long categoryId)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
             expression: n => n.Id == noteId && !n.IsDeleted)
             ?? throw new NotFoundException($"Note with Id ({noteId}) is not found");

        if (existNote.CategoryId == categoryId)
            throw new AlreadyExistException("Note is already set to this category");

        existNote.CategoryId = categoryId;
        existNote.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Notes.UpdateAsync(existNote);
        await unitOfWork.SaveAsync();

        return updated;
    }
    public async ValueTask<Note> UnsetCategoryId(long noteId)
    {
        var existNote = await unitOfWork.Notes.SelectAsync(
             expression: n => n.Id == noteId && !n.IsDeleted)
             ?? throw new NotFoundException($"Note with Id ({noteId}) is not found");

        if (existNote.CategoryId == 0 || existNote.CategoryId is null)
            throw new ArgumentIsNotValidException("Note has not set to any category");

        existNote.CategoryId = null;
        existNote.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.Notes.UpdateAsync(existNote);
        await unitOfWork.SaveAsync();

        return updated;

    }
    #endregion
}
