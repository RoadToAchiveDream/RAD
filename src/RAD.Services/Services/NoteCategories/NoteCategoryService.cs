using Microsoft.EntityFrameworkCore;
using RAD.DAL.UnintOfWorks;
using RAD.Domain.Entities;
using RAD.Services.Configurations;
using RAD.Services.Exceptions;
using RAD.Services.Extensions;
using RAD.Services.Helpers;

namespace RAD.Services.Services.NoteCategories;

public class NoteCategoryService(IUnitOfWork unitOfWork) : INoteCategoryService
{
    #region NoteCategory CRUD
    public async ValueTask<NoteCategory> CreateAsync(NoteCategory noteCategory)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => nc.Name.ToLower() == noteCategory.Name.ToLower() && !nc.IsDeleted);

        var existUser = await unitOfWork.Users.SelectAsync(
            expression: u => u.Id == HttpContextHelper.UserId && !u.IsDeleted)
            ?? throw new NotFoundException($"User is not found");

        if (existNoteCategory is not null)
            throw new AlreadyExistException("Category with this name is already exists");

        noteCategory.UserId = existUser.Id;
        noteCategory.User = existUser;
        noteCategory.CreatedByUserId = HttpContextHelper.UserId;

        var created = await unitOfWork.NoteCategories.InsertAsync(noteCategory);
        await unitOfWork.SaveAsync();

        return created;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => nc.Id == id && !nc.IsDeleted)
            ?? throw new NotFoundException($"Category with Id ({id}) is not found");

        existNoteCategory.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.NoteCategories.DeleteAsync(existNoteCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<NoteCategory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var noteCategories = unitOfWork.NoteCategories.SelectAsQueryable(
            expression: nc => !nc.IsDeleted,
            isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            noteCategories = noteCategories.Where(tc =>
                tc.Name.ToLower().Contains(search.ToLower()));

        return await noteCategories.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<NoteCategory> GetByIdAsync(long id)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => nc.Id == id && !nc.IsDeleted)
            ?? throw new NotFoundException($"category with Id ({id}) is not found");

        return existNoteCategory;
    }

    public async ValueTask<NoteCategory> UpdateAsync(long id, NoteCategory noteCategory)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
           expression: nc => nc.Id == id && !nc.IsDeleted)
           ?? throw new NotFoundException($"category with Id ({id}) is not found");

        existNoteCategory.Name = noteCategory.Name;
        existNoteCategory.UpdatedByUserId = HttpContextHelper.UserId;

        var updated = await unitOfWork.NoteCategories.UpdateAsync(existNoteCategory);
        await unitOfWork.SaveAsync();

        return updated;
    }
    #endregion

    #region NoteCategory Features
    public async ValueTask<NoteCategory> AddNoteToCategoryAsync(long categoryId, long noteId)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Id == categoryId && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted,
            includes: ["User", "Notes"])
            ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var existsNote = await unitOfWork.Notes.SelectAsync(
            expression: n => (n.Id == noteId && n.UserId == HttpContextHelper.UserId) && !n.IsDeleted,
            includes: ["User", "Category"])
            ?? throw new NotFoundException($"Note with Id ({noteId}) is not found");

        existsNote.CategoryId = categoryId;
        existsNote.Category = existCategory;
        await unitOfWork.SaveAsync();

        return await GetByIdAsync(categoryId);
    }
    public async ValueTask<NoteCategory> RemoveNoteFromCategoryAsync(long categoryId, long noteId)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
           expression: cc => (cc.Id == categoryId && cc.UserId == HttpContextHelper.UserId) && !cc.IsDeleted,
           includes: ["User", "Notes"])
           ?? throw new NotFoundException($"Category with Id ({categoryId}) is not found");

        var existsNote = await unitOfWork.Tasks.SelectAsync(
            expression: n => (n.Id == noteId && n.UserId == HttpContextHelper.UserId) && !n.IsDeleted,
            includes: ["User", "Category"])
            ?? throw new NotFoundException($"Note with Id ({noteId}) is not found");

        existsNote.CategoryId = 0;
        existsNote.Category = null;
        await unitOfWork.SaveAsync();

        return await GetByIdAsync(categoryId);
    }
    public async ValueTask<NoteCategory> GetCategoryByNameAsync(string name)
    {
        var existCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => (nc.Name.ToLower().Contains(name) && nc.UserId == HttpContextHelper.UserId) && !nc.IsDeleted,
            includes: ["User", "Notes"])
            ?? throw new NotFoundException($"Category with name ({name}) is not found");

        return existCategory;
    }
    #endregion
}
