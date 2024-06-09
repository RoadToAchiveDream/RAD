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
    public async ValueTask<NoteCategory> CreateAsync(NoteCategory noteCategory)
    {
        var existNoteCategory = await unitOfWork.NoteCategories.SelectAsync(
            expression: nc => nc.Name.ToLower() == noteCategory.Name.ToLower() && !nc.IsDeleted);

        if (existNoteCategory is not null)
            throw new AlreadyExistException("Category with this name is already exists");

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
}
