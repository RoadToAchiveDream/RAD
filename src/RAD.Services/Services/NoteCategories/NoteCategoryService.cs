using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;
using RAD.Services.Services.Notes;
using RAD.Services.Services.Users;

namespace RAD.Services.Services.NoteCategories;

public class NoteCategoryService(IUserService userService, INoteService noteService, IUnitOfWork unitOfWork) : INoteCategoryService
{
    #region NoteCategory CRUD
    public async ValueTask<bool> CreateAsync(NoteCategory noteCategory)
    {
        var existUser = await userService.GetByIdAsync(HttpContextHelper.UserId);

        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Name.ToLower() == noteCategory.Name.ToLower() && nc.UserId == HttpContextHelper.UserId)
            && !nc.IsDeleted);

        if (existNoteCategory is not null)
            throw new AlreadyExistException("Category with this name is already exists");

        noteCategory.UserId = existUser.Id;
        noteCategory.User = existUser;
        noteCategory.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.NoteCategories.InsertAsync(noteCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Id == id && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted)
            ?? throw new NotFoundException($"Category with Id ({id}) is not found");

        existNoteCategory.DeletedByUserId = HttpContextHelper.UserId;

        await unitOfWork.NoteCategories.DeleteAsync(existNoteCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<NoteCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var noteCategories = unitOfWork.NoteCategories.SelectAsQueryable(
            expression: nc => !nc.IsDeleted && nc.UserId == HttpContextHelper.UserId,
            includes: ["Notes"],
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            noteCategories = noteCategories.Where(tc =>
                tc.Name.ToLower().Contains(search.ToLower()));

        foreach (var item in noteCategories)
        {
            var filteredNotes = item.Notes.Where(note => !note.IsDeleted).ToList();
            item.Notes = filteredNotes;
        }

        return await noteCategories.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<NoteCategory> GetByIdAsync(long id)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Id == id && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted,
            includes: ["Notes"])
            ?? throw new NotFoundException($"category with Id ({id}) is not found");

        var filteredNotes = existNoteCategory.Notes.Where(note => !note.IsDeleted);
        existNoteCategory.Notes = filteredNotes;

        return existNoteCategory;
    }

    public async ValueTask<NoteCategory> UpdateAsync(long id, NoteCategory noteCategory)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
           expression: nc => (nc.Id == id && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted,
           includes: ["User"])
           ?? throw new NotFoundException($"category with Id ({id}) is not found");

        existNoteCategory.Name = noteCategory.Name;
        existNoteCategory.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.NoteCategories.UpdateAsync(existNoteCategory);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region NoteCategory Features
    public async ValueTask<bool> AddNoteToCategoryAsync(long categoryId, long noteId)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Id == categoryId && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted)
            ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var set = await noteService.SetCategoryId(noteId, categoryId);

        return true;
    }
    public async ValueTask<bool> RemoveNoteFromCategoryAsync(long categoryId, long noteId)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Id == categoryId && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted)
            ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var unset = await noteService.UnsetCategoryId(noteId);

        return true;
    }
    public async ValueTask<NoteCategory> GetCategoryByNameAsync(string name)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Name.ToLower().Contains(name) && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted,
            includes: ["User", "Notes"])
            ?? throw new NotFoundException($"Category with name ({name}) is not found");

        var filteredNotes = existCategory.Notes.Where(note => !note.IsDeleted);
        existCategory.Notes = filteredNotes;

        return existCategory;
    }
    #endregion
}
