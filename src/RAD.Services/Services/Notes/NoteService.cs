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
                user.Title.ToLower().Contains(search.ToLower()) ||
                user.Content.ToLower().Contains(search.ToLower()));

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

        var updated = await unitOfWork.Notes.UpdateAsync(existNote);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region Note Feateures
    public async ValueTask<Note> SetCategory(long id, string category)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
            expression: note => note.Id == id && !note.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        if (existsNote.Category.ToString() == category)
            throw new AlreadyExistException("Same Category is already set for this note");

        switch (category)
        {
            case "Personal":
                existsNote.Category = NoteCategory.Personal;
                break;
            case "General":
                existsNote.Category = NoteCategory.General;
                break;
            case "Work":
                existsNote.Category = NoteCategory.Work;
                break;
            case "Study":
                existsNote.Category = NoteCategory.Study;
                break;
            case "Other":
                existsNote.Category = NoteCategory.Other;
                break;
            default: throw new NotFoundException($"This category ({category}) is not valid");
        }

        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }
    public async ValueTask<Note> SetPinned(long id)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
            expression: note => note.Id == id && !note.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        existsNote.IsPinned = true;
        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }

    public async ValueTask<Note> UnsetCategory(long id)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
            expression: note => note.Id == id && !note.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        if (existsNote.Category == NoteCategory.None)
            throw new AlreadyExistException("Already unset of not set Category yet");


        existsNote.Category = NoteCategory.None;
        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }
    public async ValueTask<Note> UnsetPinned(long id)
    {
        var existsNote = await unitOfWork.Notes.SelectAsync(
           expression: note => note.Id == id && !note.IsDeleted,
           includes: ["User"])
           ?? throw new NotFoundException($"Note with Id ({id}) is not found");

        existsNote.IsPinned = false;
        existsNote.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.SaveAsync();

        return existsNote;
    }
    public async ValueTask<IEnumerable<Note>> GetByCategory(string category)
    {
        var Notes = await unitOfWork.Notes.SelectAsEnumerableAsync(
            expression: note => note.Category.ToString() == category && !note.IsDeleted,
            includes: ["User"])
            ?? throw new NotFoundException($"Notes according to this category ({category}) are not found");

        return Notes;
    }
    #endregion
}
